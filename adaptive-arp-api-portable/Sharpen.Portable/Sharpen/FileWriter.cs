namespace Sharpen
{
	using System;
	using System.IO;

	internal class FileWriter : StreamWriter
	{
		/*
        public FileWriter (FilePath path) : base(new Stream(path.GetPath ()))
		{
		}
        */

        public FileWriter(Stream stream) : base(stream)
        {

        }
		
		public FileWriter Append (string sequence)
		{
			Write (sequence);
			return this;
		}
	}
}
