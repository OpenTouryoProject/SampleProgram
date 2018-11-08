using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Jose;
using Security.Cryptography;

using Touryo.Infrastructure.Public.Str;

namespace jose_jwt_Sample
{
    /// <summary>
    /// jose-jwt - マイクロソフト系技術情報 Wiki
    /// https://techinfoofmicrosofttech.osscons.jp/index.php?jose-jwt
    /// 
    /// Certificates
    /// https://github.com/OpenTouryoProject/OpenTouryo/tree/develop/root/files/resource/X509
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables

            IDictionary<string, object> headers = null;
            IDictionary<string, object> payload = null;

            OperatingSystem os = Environment.OSVersion;

            string token = "";

            byte[] secretKey = null;
            byte[] x = null;
            byte[] y = null;
            byte[] d = null;

            string privateX509Path = "";
            string publicX509Path = "";
            X509Certificate2 publicKeyX509 = null;
            X509Certificate2 privateKeyX509 = null;

            CngKey publicKeyOfCng = null;
            CngKey privateKeyOfCng = null;
            #endregion

            payload = new Dictionary<string, object>()
            {
                { "sub", "mr.x@contoso.com" },
                { "exp", 1300819380 }
            };

            #region Unsecured JWT
            // Creating Plaintext (unprotected) Tokens
            // https://github.com/dvsekhvalnov/jose-jwt#creating-plaintext-unprotected-tokens
            token = "";
            token = JWT.Encode(payload, null, JwsAlgorithm.none);
            Program.MyWriteLine("JwsAlgorithm.none: " + token);
            #endregion

            #region JWS (Creating signed Tokens)
            // https://github.com/dvsekhvalnov/jose-jwt#creating-signed-tokens

            #region HS-* family
            // HS256, HS384, HS512
            // https://github.com/dvsekhvalnov/jose-jwt#hs--family
            secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
            token = "";
            token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            Program.VeriyResult("JwsAlgorithm.HS256: ", token, secretKey);
            #endregion

            #region RS-* and PS-* family
            // RS256, RS384, RS512 and PS256, PS384, PS512
            // https://github.com/dvsekhvalnov/jose-jwt#rs--and-ps--family
            // X509Certificate2 x509Certificate2 = new X509Certificate2();

            privateX509Path = @"SHA256RSA.pfx";
            publicX509Path = @"SHA256RSA.cer";
            privateKeyX509 = new X509Certificate2(privateX509Path, "test",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            publicKeyX509 = new X509Certificate2(publicX509Path, "",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);

            token = "";
            token = Jose.JWT.Encode(payload, privateKeyX509.PrivateKey, JwsAlgorithm.RS256);
            Program.VeriyResult("JwsAlgorithm.RS256: ", token, publicKeyX509.PublicKey.Key);
            #endregion

            #region ES- * family
            // ES256, ES384, ES512 ECDSA signatures
            // https://github.com/dvsekhvalnov/jose-jwt#es---family
            if (os.Platform == PlatformID.Win32NT)
            {
                x = new byte[] { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };
                y = new byte[] { 131, 116, 8, 14, 22, 150, 18, 75, 24, 181, 159, 78, 90, 51, 71, 159, 214, 186, 250, 47, 207, 246, 142, 127, 54, 183, 72, 72, 253, 21, 88, 53 };
                d = new byte[] { 42, 148, 231, 48, 225, 196, 166, 201, 23, 190, 229, 199, 20, 39, 226, 70, 209, 148, 29, 70, 125, 14, 174, 66, 9, 198, 80, 251, 95, 107, 98, 206 };

                privateKeyOfCng = EccKey.New(x, y, d);
                publicKeyOfCng = EccKey.New(x, y);
                token = "";
                token = JWT.Encode(payload, privateKeyOfCng, JwsAlgorithm.ES256);
                Program.VeriyResult("JwsAlgorithm.ES256: ", token, publicKeyOfCng);
            }
            else // == PlatformID.Unix
            { 
                privateX509Path = @"SHA256ECDSA.pfx";
                publicX509Path = @"SHA256ECDSA.cer";
                privateKeyX509 = new X509Certificate2(privateX509Path, "test",
                    X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                publicKeyX509 = new X509Certificate2(publicX509Path, "",
                    X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
                token = "";
                token = JWT.Encode(payload, privateKeyX509.GetECDsaPrivateKey(), JwsAlgorithm.ES256);
                Program.VeriyResult("JwsAlgorithm.ES256: ", token, publicKeyX509.GetECDsaPublicKey());
            }

            #endregion

            #endregion

            #region JWE (Creating encrypted Tokens)
            // https://github.com/dvsekhvalnov/jose-jwt#creating-encrypted-tokens

            #region RSA-* key management family of algorithms
            // RSA-OAEP-256, RSA-OAEP and RSA1_5 key
            https://github.com/dvsekhvalnov/jose-jwt#rsa--key-management-family-of-algorithms

            privateX509Path = @"SHA256RSA.pfx";
            publicX509Path = @"SHA256RSA.cer";
            privateKeyX509 = new X509Certificate2(privateX509Path, "test",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            publicKeyX509 = new X509Certificate2(publicX509Path, "",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);

            // RSAES-PKCS1-v1_5 and AES_128_CBC_HMAC_SHA_256
            token = "";
            token = JWT.Encode(payload, publicKeyX509.PublicKey.Key, JweAlgorithm.RSA1_5, JweEncryption.A128CBC_HS256);
            Program.VeriyResult("JweAlgorithm.RSA1_5, JweEncryption.A128CBC_HS256: ", token, privateKeyX509.PrivateKey);

            // RSAES-OAEP and AES GCM
            token = "";
            token = JWT.Encode(payload, publicKeyX509.PublicKey.Key, JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM);
            Program.VeriyResult("JweAlgorithm.RSA_OAEP, JweEncryption.A256GCM: ", token, privateKeyX509.PrivateKey);
            #endregion

            #region Other key management family of algorithms

            secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 164, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };

            #region DIR direct pre-shared symmetric key family of algorithms
            // https://github.com/dvsekhvalnov/jose-jwt#dir-direct-pre-shared-symmetric-key-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, secretKey, JweAlgorithm.DIR, JweEncryption.A128CBC_HS256);
            Program.VeriyResult("JweAlgorithm.DIR, JweEncryption.A128CBC_HS256: ", token, secretKey);
            #endregion

            #region AES Key Wrap key management family of algorithms
            // AES128KW, AES192KW and AES256KW key management
            // https://github.com/dvsekhvalnov/jose-jwt#aes-key-wrap-key-management-family-of-algorithms
            token = "";
            token = JWT.Encode(payload, secretKey, JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
            Program.VeriyResult("JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512: ", token, secretKey);
            #endregion

            #region AES GCM Key Wrap key management family of algorithms
            // AES128GCMKW, AES192GCMKW and AES256GCMKW key management
            // https://github.com/dvsekhvalnov/jose-jwt#aes-gcm-key-wrap-key-management-family-of-algorithms
            if (os.Platform == PlatformID.Win32NT)
            {
                token = "";
                token = JWT.Encode(payload, secretKey, JweAlgorithm.A256GCMKW, JweEncryption.A256CBC_HS512);
                Program.VeriyResult("JweAlgorithm.A256GCMKW, JweEncryption.A256CBC_HS512: ", token, secretKey);
            }
            else // == PlatformID.Unix
            {
                // Unhandled Exception: System.DllNotFoundException: Unable to load DLL 'bcrypt.dll': The specified module or one of its dependencies could not be found.
            }
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
            //Program.VeriyResult("JweAlgorithm.ECDH_ES, JweEncryption.A256GCM: ", token, publicKeyOfCng);
            #endregion

            #region PBES2 using HMAC SHA with AES Key Wrap key management family of algorithms
            token = "";
            token = JWT.Encode(payload, "top secret", JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
            Program.VeriyResult("JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512: ", token, "top secret");
            #endregion

            #endregion

            #endregion

            Program.MyWriteLine("----------------------------------------------------------------------------------------------------");

            #region Additional utilities
            // https://github.com/dvsekhvalnov/jose-jwt#additional-utilities

            #region Adding extra headers
            // https://github.com/dvsekhvalnov/jose-jwt#adding-extra-headers

            headers = new Dictionary<string, object>()
            {
                { "typ", "JWT" },
                { "cty", "JWT" },
                { "keyid", "111-222-333"}
            };

            privateX509Path = @"SHA256RSA.pfx";
            publicX509Path = @"SHA256RSA.cer";
            privateKeyX509 = new X509Certificate2(privateX509Path, "test",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
            publicKeyX509 = new X509Certificate2(publicX509Path, "",
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);

            token = "";
            token = Jose.JWT.Encode(payload, privateKeyX509.PrivateKey, JwsAlgorithm.RS256, extraHeaders: headers);
            Program.VeriyResult("Adding extra headers to RS256: ", token, privateKeyX509.PrivateKey);
            #endregion

            #region Strict validation
            // https://github.com/dvsekhvalnov/jose-jwt#strict-validation
            // 厳密な検証では、Algorithmを指定可能
            Program.MyWriteLine("Strict validation(RS256): " + JWT.Decode(token, privateKeyX509.PrivateKey, JwsAlgorithm.RS256));
            #endregion

            #region Two-phase validation
            // https://github.com/dvsekhvalnov/jose-jwt#two-phase-validation
            // ヘッダのkeyidクレームからキーを取り出して復号化する方法。
            //headers = Jose.JWT.Headers(token);
            // ・・・
            //string hoge = Jose.JWT.Decode(token, "key");
            #endregion

            #region Working with binary payload
            // https://github.com/dvsekhvalnov/jose-jwt#working-with-binary-payload
            #endregion

            #endregion

            #region Settings
            // https://github.com/dvsekhvalnov/jose-jwt#settings
            // グローバル設定

            #region Example of JWTSettings
            // https://github.com/dvsekhvalnov/jose-jwt#example-of-jwtsettings

            #endregion

            #region Customizing json <-> object parsing & mapping
            // https://github.com/dvsekhvalnov/jose-jwt#customizing-json---object-parsing--mapping
            // マッピング
            // https://github.com/dvsekhvalnov/jose-jwt#example-of-newtonsoftjson-mapper
            // https://github.com/dvsekhvalnov/jose-jwt#example-of-servicestack-mapper

            #endregion

            #region Customizing algorithm implementations
            // https://github.com/dvsekhvalnov/jose-jwt#customizing-algorithm-implementations
            // https://github.com/dvsekhvalnov/jose-jwt#example-of-custom-algorithm-implementation
            #endregion

            #region Providing aliases
            // https://github.com/dvsekhvalnov/jose-jwt#providing-aliases
            #endregion

            #endregion

            #region Dealing with keys
            // https://github.com/dvsekhvalnov/jose-jwt#dealing-with-keys
            // https://github.com/dvsekhvalnov/jose-jwt#rsacryptoserviceprovider
            // - http://stackoverflow.com/questions/7444586/how-can-i-sign-a-file-using-rsa-and-sha256-with-net
            // - http://hintdesk.com/c-how-to-fix-invalid-algorithm-specified-when-signing-with-sha256/
            // https://github.com/dvsekhvalnov/jose-jwt#if-you-have-only-rsa-private-key
            // - http://www.donaldsbaconbytes.com/2016/08/create-jwt-with-a-private-rsa-key/
            #endregion

            #region Strong-Named assembly
            // https://github.com/dvsekhvalnov/jose-jwt#strong-named-assembly
            // - https://github.com/dvsekhvalnov/jose-jwt/issues/5
            // - https://github.com/brutaldev/StrongNameSigner
            #endregion

            #region More examples
            // https://github.com/dvsekhvalnov/jose-jwt#more-examples
            // https://github.com/dvsekhvalnov/jose-jwt/blob/master/UnitTests/TestSuite.cs
            #endregion

            Console.ReadKey();
        }

        /// <summary>VeriyResult</summary>
        /// <param name="testId">string</param>
        /// <param name="token">string</param>
        /// <param name="key">object</param>
        private static void VeriyResult(string testId, string token, object key)
        {
            Program.MyWriteLine(testId + token);

            string[] aryStr = token.Split('.');

            Program.MyWriteLine("JWT Header: " + CustomEncode.ByteToString(
                CustomEncode.FromBase64UrlString(aryStr[0]), CustomEncode.UTF_8));

            if (3 < aryStr.Length)
            {
                // JWE
                Program.MyWriteLine("- JWE Encrypted Key: " + aryStr[1]);
                Program.MyWriteLine("- JWE Initialization Vector: " + aryStr[2]);
                Program.MyWriteLine("- JWE Ciphertext: " + aryStr[3]);
                Program.MyWriteLine("- JWE Authentication Tag: " + aryStr[4]);
            }

            Program.MyWriteLine("Decoded: " + JWT.Decode(token, key));
        }

        /// <summary>MyWriteLine</summary>
        /// <param name="s">string</param>
        private static void MyWriteLine(string s)
        {
            Console.WriteLine(s);
            Debug.WriteLine(s);
        }
    }
}
