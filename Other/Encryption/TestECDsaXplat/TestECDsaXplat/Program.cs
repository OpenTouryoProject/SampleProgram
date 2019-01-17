using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace TestECDsaXplat
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[] { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };

            byte[] x = new byte[] { 4, 114, 29, 223, 58, 3, 191, 170, 67, 128, 229, 33, 242, 178, 157, 150, 133, 25, 209, 139, 166, 69, 55, 26, 84, 48, 169, 165, 67, 232, 98, 9 };
            byte[] y = new byte[] { 131, 116, 8, 14, 22, 150, 18, 75, 24, 181, 159, 78, 90, 51, 71, 159, 214, 186, 250, 47, 207, 246, 142, 127, 54, 183, 72, 72, 253, 21, 88, 53 };
            byte[] d = new byte[] { 42, 148, 231, 48, 225, 196, 166, 201, 23, 190, 229, 199, 20, 39, 226, 70, 209, 148, 29, 70, 125, 14, 174, 66, 9, 198, 80, 251, 95, 107, 98, 206 };

            ECParameters ecp = new ECParameters();
            ecp.Curve = ECCurve.NamedCurves.nistP256;
            ecp.Q.X = x;
            ecp.Q.Y = y;
            ecp.D = d;

            ECDsaCng ecDsaCng = new ECDsaCng();
            ecDsaCng.ImportParameters(ecp);
            CngKey cngKey = ecDsaCng.Key;

            ECDsaCng eCDsaCng = new ECDsaCng(cngKey);

            byte[] sign = eCDsaCng.SignData(data);
            Console.WriteLine(eCDsaCng.VerifyData(data, sign));
            Console.ReadKey();
        }
    }
}
