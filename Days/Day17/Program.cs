using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Program
    {

        private static int[] register = new int[3];
        private static string output = string.Empty;
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputSeventeen.txt";
            var lines = File.ReadAllLines(filePath);

            register[0] = int.Parse(lines[0].Replace("Register A: ", string.Empty));
            register[1] = int.Parse(lines[1].Replace("Register B: ", string.Empty));
            register[2] = int.Parse(lines[2].Replace("Register C: ", string.Empty));

            var program = lines[4].Replace("Program: ", string.Empty).Split(',').Select(int.Parse).ToArray();

            var pointer = 0;

            do
            {
                pointer = ExecuteInstruction(pointer, program[pointer], program[pointer + 1]);
            } while (pointer < program.Length);

            Console.WriteLine(output.Substring(0, output.Length - 1));
        }

        static int ExecuteInstruction(int pointer, int opcode, int operand)
        {
            switch (opcode)
            {
                case 0: return Adv(pointer, Combine(operand));
                case 1: return Bxl(pointer, operand);
                case 2: return Bst(pointer, Combine(operand));
                case 3: return Jnz(pointer, operand);
                case 4: return Bxc(pointer);
                case 5: return Ovt(pointer, Combine(operand));
                case 6: return Bdv(pointer, Combine(operand));
                case 7: return Cdv(pointer, Combine(operand));
                default: return -1;
            }
        }

        static int Combine(int literalOperand)
        {
            var comboOperand = literalOperand;
            if (literalOperand > 3)
                comboOperand = register[comboOperand - 4];
            return comboOperand;
        }

        static int Adv(int pointer, int comboOperand)
        {
            register[0] /= (int)Math.Pow(2, comboOperand);
            return pointer + 2;
        }

        static int Bxl(int pointer, int literalOperand)
        {
            register[1] = register[1] ^ literalOperand;
            return pointer + 2;
        }

        static int Bst(int pointer, int comboOperand)
        {
            register[1] = comboOperand % 8;
            return pointer + 2;
        }

        static int Jnz(int pointer, int literalOperand)
        {
            if (register[0] == 0)
                return pointer + 2;
            return literalOperand;
        }

        static int Bxc(int pointer)
        {
            register[1] = register[1] ^ register[2];
            return pointer + 2;
        }

        static int Ovt(int pointer, int comboOperand)
        {
            output += $"{comboOperand % 8},";
            return pointer + 2;
        }

        static int Bdv(int pointer, int comboOperand)
        {
            register[1] = register[0] / (int)Math.Pow(2, comboOperand);
            return pointer + 2;
        }

        static int Cdv(int pointer, int comboOperand)
        {
            register[2] = register[0] / (int)Math.Pow(2, comboOperand);
            return pointer + 2;
        }

    }
}
