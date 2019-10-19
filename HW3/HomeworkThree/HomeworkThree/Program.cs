using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeworkThree;

namespace HomeworkThree
{
    public class Program
    {
        /** Print the command line usage for this program */
        private static void printUsage()
        {
            Console.Write("Usage is:\n" +
                "\tjava Main C inputfile outputfile\n\n" +
                "Where:" +
                "  C is the column number to fit to\n" +
                "  inputfile is the input text file \n" +
                "  outputfile is the new output file base name containing the wrapped text.\n" +
                "e.g. java Main 72 myfile.txt myfile_wrapped.txt");
        }
        static void Main(string[] args)
        {
            int c = 72;                     // Column length to wrap to
            string inputFilename = null;
            string outputFilename = "output.txt";
            string scanner = null;


            // Read the command line arguments ...
            if (args.Length != 3)
            {
                printUsage();
                Environment.Exit(1);
            }
            try
            {
                c = Convert.ToInt32(args[0]);
                inputFilename = args[1];
                outputFilename = args[2];
                scanner = File.ReadAllText(inputFilename);

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Could not find the input file.");
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something is wrong with the input.");
                printUsage();
                Environment.Exit(1);
            }

            // Read words and their lengths into these vectors
            IQueueInterface<string> words = new LinkedQueue<string>();

            // Read input file, tokenize by whitespace
            string[] wordString = Regex.Split(scanner, @"\s+");
            foreach (string word in wordString)
            {
                words.Push(word);
            }

            // At this point the input text file has now been placed, word by word, into a FIFO queue
            // Each word does not have whitespaces included, but does have punctuation, numbers, etc.

            /* ------------------ Start here ---------------------- */

            // As an example, do a simple wrap
            int spacesRemaining = wrapSimply(words, c, outputFilename);
            Console.WriteLine("Total spaces remaining (Greedy): " + spacesRemaining);
        } // End main()

        /*-----------------------------------------------------------------------
            Greedy Algorithm (Non-optimal i.e. approximate or heuristic solution)
          -----------------------------------------------------------------------*/

        /**
            As an example only, write out the file directly to the output with _simple_ wrapping,
            i.e. adding spaces between words and moving to the next line if a word would go past the
            indicated column number C.  This will fail if any word length exceeds the column limit C,
            but it still goes ahead and just puts one word on that line.
        */
        private static int wrapSimply(IQueueInterface<String> words, int columnLength, String outputFilename)
        {
            //Writing into file, setting writeFile to null
            System.IO.TextWriter writeFile = null;
            //Testing for errors
            try
            {
                writeFile = new StreamWriter(outputFilename);
            }
            catch (FileNotFoundException e)
            {
                Console.Write("Cannot create or open " + outputFilename +
                            " for writing.  Using standard output instead.");
            }

            int col = 1;
            int spacesRemaining = 0;            // Running count of spaces left at the end of lines
            while (!words.IsEmpty())
            {
                string str = words.Peek();
                int len = str.Length;
                // See if we need to wrap to the next line
                if (col == 1)
                {
                    writeFile.Write(str);
                    col += len;
                    words.Pop();
                }
                else if ((col + len) >= columnLength)
                {
                    // go to the next line
                    writeFile.Write(Environment.NewLine);
                    spacesRemaining += (columnLength - col) + 1;
                    col = 1;
                }
                else
                {   // Typical case of printing the next word on the same line
                    writeFile.Write(" ");
                    writeFile.Write(str);
                    col += (len + 1);
                    words.Pop();
                }

            }
            writeFile.Write(Environment.NewLine);
            writeFile.Flush();
            writeFile.Close();
            return spacesRemaining;
        } // end wrapSimply
    }
}

/**
 * Text wrapper (from CS 345 Lab 3)
 */

/**
 *  The main program. 
 *
 *@param  args  The command line arguments, see usage notes
 */
