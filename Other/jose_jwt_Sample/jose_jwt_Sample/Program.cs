using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Jose;
using Security.Cryptography;

namespace jose_jwt_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "";

            byte[] secretKey = null;

            CngKey publicKeyOfCng = null;
            CngKey privateKeyOfCng = null;
            X509Certificate2 publicKeyX509 = null;
            X509Certificate2 privateKeyX509 = null;

            byte[] x = null;
            byte[] y = null;
            byte[] d = null;

            Dictionary<string, object> payload = new Dictionary<string, object>()
            {
                { "sub", "mr.x@contoso.com" },
                { "exp", 1300819380 }
            };

            // Creating Plaintext (unprotected) Tokens
            // https://github.com/dvsekhvalnov/jose-jwt#creating-plaintext-unprotected-tokens
            token = "";
            token = JWT.Encode(payload, null, JwsAlgorithm.none);
            Debug.WriteLine("JwsAlgorithm.none: " + token);

            #region JWS (Creating signed Tokens)
            // https://github.com/dvsekhvalnov/jose-jwt#creating-signed-tokens

            #region HS-* family
            // HS256, HS384, HS512
            // https://github.com/dvsekhvalnov/jose-jwt#hs--family
            secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            token = "";
            token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            Debug.WriteLine("JwsAlgorithm.HS256: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, secretKey));
            #endregion

            #region RS-* and PS-* family
            // RS256, RS384, RS512 and PS256, PS384, PS512
            // https://github.com/dvsekhvalnov/jose-jwt#rs--and-ps--family
            // X509Certificate2 x509Certificate2 = new X509Certificate2();
            string privateX509Path = @"SHA256RSA.pfx";
            string publicX509Path = @"SHA256RSA.cer";
            privateKeyX509 = new X509Certificate2(privateX509Path, "test",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            publicKeyX509 = new X509Certificate2(publicX509Path, "",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            token = "";
            token = Jose.JWT.Encode(payload, privateKeyX509.PrivateKey, JwsAlgorithm.RS256);
            Debug.WriteLine("JwsAlgorithm.RS256: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, publicKeyX509.PublicKey.Key));
            #endregion

            #region ES- * family
            // ES256, ES384, ES512 ECDSA signatures
            // https://github.com/dvsekhvalnov/jose-jwt#es---family
            x = new byte[] { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };
            y = new byte[] { 131, 116, 8, 14, 22, 150, 18, 75, 24, 181, 159, 78, 90, 51, 71, 159, 214, 186, 250, 47, 207, 246, 142, 127, 54, 183, 72, 72, 253, 21, 88, 53 };
            d = new byte[] { 42, 148, 231, 48, 225, 196, 166, 201, 23, 190, 229, 199, 20, 39, 226, 70, 209, 148, 29, 70, 125, 14, 174, 66, 9, 198, 80, 251, 95, 107, 98, 206 };

            privateKeyOfCng = EccKey.New(x, y, d);
            publicKeyOfCng = EccKey.New(x, y);
            token = "";
            token = JWT.Encode(payload, privateKeyOfCng, JwsAlgorithm.ES256);
            Debug.WriteLine("JwsAlgorithm.ES256: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, publicKeyOfCng));
            #endregion

            #endregion

            #region JWE (Creating encrypted Tokens)
            // https://github.com/dvsekhvalnov/jose-jwt#creating-encrypted-tokens

            #region RSA-* key management family of algorithms
            // RSA-OAEP-256, RSA-OAEP and RSA1_5 key
            https://github.com/dvsekhvalnov/jose-jwt#rsa--key-management-family-of-algorithms
            #endregion

            secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

            #region DIR direct pre-shared symmetric key family of algorithms
            // https://github.com/dvsekhvalnov/jose-jwt#dir-direct-pre-shared-symmetric-key-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, secretKey, JweAlgorithm.DIR, JweEncryption.A128CBC_HS256);
            Debug.WriteLine("JweAlgorithm.DIR, JweEncryption.A128CBC_HS256: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, secretKey));
            #endregion

            #region AES Key Wrap key management family of algorithms
            // AES128KW, AES192KW and AES256KW key management
            // https://github.com/dvsekhvalnov/jose-jwt#aes-key-wrap-key-management-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, secretKey, JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
            Debug.WriteLine("JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, secretKey));
            #endregion

            #region AES GCM Key Wrap key management family of algorithms
            // AES128GCMKW, AES192GCMKW and AES256GCMKW key management
            // https://github.com/dvsekhvalnov/jose-jwt#aes-gcm-key-wrap-key-management-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, secretKey, JweAlgorithm.A256GCMKW, JweEncryption.A256CBC_HS512);
            Debug.WriteLine("JweAlgorithm.A256GCMKW, JweEncryption.A256CBC_HS512: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, secretKey));
            #endregion

            #region ECDH-ES and ECDH-ES with AES Key Wrap key management family of algorithms
            // System.NotImplementedException: 'not yet'

            //// ECDH-ES and ECDH-ES+A128KW, ECDH-ES+A192KW, ECDH-ES+A256KW key management
            //// https://github.com/dvsekhvalnov/jose-jwt#ecdh-es-and-ecdh-es-with-aes-key-wrap-key-management-family-of-algorithms
            //x = new byte[] { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };
            //y = new byte[] { 131, 116, 8, 14, 22, 150, 18, 75, 24, 181, 159, 78, 90, 51, 71, 159, 214, 186, 250, 47, 207, 246, 142, 127, 54, 183, 72, 72, 253, 21, 88, 53 };
            //publicKeyOfCng = EccKey.New(x, y, usage: CngKeyUsages.KeyAgreement);
            //token = "";
            //token = JWT.Encode(payload, publicKeyOfCng, JweAlgorithm.ECDH_ES, JweEncryption.A256GCM);
            //Debug.WriteLine("JweAlgorithm.ECDH_ES, JweEncryption.A256GCM: " + token);
            //Debug.WriteLine("Decoded: " + JWT.Decode(token, publicKeyOfCng));
            #endregion

            #region PBES2 using HMAC SHA with AES Key Wrap key management family of algorithms
            // PBES2-HS256+A128KW, PBES2-HS384+A192KW, PBES2-HS512+A256KW key management
            // https://github.com/dvsekhvalnov/jose-jwt#pbes2-using-hmac-sha-with-aes-key-wrap-key-management-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, "top secret", JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
            Debug.WriteLine("JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512: " + token);
            Debug.WriteLine("Decoded: " + JWT.Decode(token, "top secret"));
            #endregion

            #endregion

            #region Additional utilities

            #endregion
        }
    }
}
