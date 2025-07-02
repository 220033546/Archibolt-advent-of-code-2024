using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Archibolt\Documents\File\inputTwentyFour.txt";
            var lines = File.ReadAllLines(filePath);

            var wires = new SortedDictionary<string, Wire>();
            var gates = new List<Gate>();

            // read all wires and gates
            foreach (var line in lines)
            {
                if (line.Contains("->"))
                {
                    var elements = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    AddWire(elements[0], wires);
                    AddWire(elements[2], wires);
                    AddWire(elements[4], wires);

                    gates.Add(new Gate(wires[elements[0]], wires[elements[2]], wires[elements[4]], elements[1]));
                }
            }

            // set initial values
            foreach (var line in lines)
            {
                if (line.Contains(':'))
                {
                    var values = line.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                    wires[values[0]].Value = values[1].Equals("1");
                }
            }

            SimulateGates(gates);

            Console.WriteLine(ProduceNumberFor("z", wires));
        }

        static void AddWire(string wireName, SortedDictionary<string, Wire> wires)
        {
            if (!wires.ContainsKey(wireName))
                wires.Add(wireName, new Wire(wireName, null));
        }

        static void SimulateGates(List<Gate> gates)
        {
            bool allReady;
            do
            {
                allReady = true;
                foreach (var gate in gates)
                {
                    if (!gate.Process())
                    {
                        allReady = false;
                    }
                }
            } while (!allReady);
        }

        static long ProduceNumberFor(string wireType, SortedDictionary<string, Wire> wires)
        {
            var result = string.Empty;
            foreach (var wireName in wires.Keys)
            {
                if (wireName.StartsWith(wireType))
                {
                    result = string.Format("{0}{1}", wires[wireName].Value.Value ? 1 : 0, result);
                }
            }
            return Convert.ToInt64(result, 2);
        }
    }

    class Gate
    {
        public Wire[] Inputs { get; }
        public Wire Output { get; }
        public string Operation { get; }
        public bool Ready { get; set; }

        public Gate(Wire in1, Wire in2, Wire output, string op)
        {
            Inputs = new Wire[] { in1, in2 };
            Output = output;
            Operation = op;
            Ready = false;
        }

        public bool Process()
        {
            if (Ready) return true;

            if (!Inputs[0].Value.HasValue || !Inputs[1].Value.HasValue)
                return false;

            switch (Operation)
            {
                case "AND":
                    Output.Value = Inputs[0].Value.Value && Inputs[1].Value.Value;
                    break;
                case "OR":
                    Output.Value = Inputs[0].Value.Value || Inputs[1].Value.Value;
                    break;
                case "XOR":
                    Output.Value = Inputs[0].Value.Value != Inputs[1].Value.Value;
                    break;
            }

            Ready = true;
            return true;
        }
    }

    class Wire
    {
        public string Name { get; }
        public bool? Value { get; set; }

        public Wire(string name, bool? value)
        {
            Name = name;
            Value = value;
        }
    }
}
