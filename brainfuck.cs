using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		Dictionary<char, Action<VirtualMachine>> commands  = new Dictionary<char, Action<VirtualMachine>>();
		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			InstructionPointer = 0;
			MemoryPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			commands.Add(symbol, execute);
		}

		public void Run()
		{
			for(; InstructionPointer < Instructions.Length; InstructionPointer++) { 
				var command = Instructions[InstructionPointer];
				if (commands.ContainsKey(command)) commands[command](this);
			}
		}
	}
}