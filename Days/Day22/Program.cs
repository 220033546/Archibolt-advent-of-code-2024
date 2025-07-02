using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwentyTwo.txt";
            var secrets = File.ReadAllLines(filePath)
                             .Select(line => long.Parse(line))
                             .ToArray();

            var answer = 0L;
            for (var i = 0; i < secrets.Length; i++)
            {
                for (var j = 0; j < 2000; j++)
                {
                    secrets[i] = pseudo(secrets[i]);
                }
                answer += secrets[i];
            }

            Console.WriteLine(answer);
        }

        static long pseudo(long secret)
        {
            secret = mix(secret, secret * 64);
            secret = prune(secret);

            secret = mix(secret, secret / 32);
            secret = prune(secret);

            secret = mix(secret, secret * 2048);
            secret = prune(secret);

            return secret;
        }

        static long mix(long secret, long value)
        {
            return secret ^ value;
        }

        static long prune(long secret)
        {
            return secret % 16777216;
        }


    }
}
