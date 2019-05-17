using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace cab301_assignment2
{
    class Program
    {
        static readonly Random r = new Random();
        static int count1;
        static int count2;
        static int count3;
        static int count4;

        static void Main(string[] args) {
            bool test = true; // true for test cases
            bool full = true; // run tests for time and count, or just time
            int numVals = 10, jump = 10, reps = 10;
            if (test) Test();
            else {
                if (full) CountExperiment(numVals, jump);
                TimeExperiment(numVals, jump, reps);
            }
        }

        static void CountExperiment(int numVals, int jump) {
            StringBuilder rCount = new StringBuilder();
            int[] a;
            int n;

            rCount.Append("n");
            rCount.Append("\t");
            rCount.Append("count.1");
            rCount.Append("\t");
            rCount.Append("count.2");
            rCount.Append("\n");

            for (int i = 0; i < numVals; i++) {
                n = i * jump;
                a = Generate(n);
                count1 = 0;
                count2 = 0;

                MinDistance1Count(a);
                MinDistance2Count(a);

                rCount.Append(n.ToString());
                rCount.Append("\t");
                rCount.Append(count1.ToString());
                rCount.Append("\t");
                rCount.Append(count2.ToString());
                rCount.Append("\n");
            }

            File.WriteAllText("D:/Documents/subjects/CAB301/Assignment2/rCount", rCount.ToString());
        }

        static void TimeExperiment(int numVals, int jump, int reps) {
            StringBuilder rTime = new StringBuilder();
            Stopwatch s = new Stopwatch();
            long t1, t2;
            int n;
            int[] a;
            
             rTime.Append("n");
            rTime.Append("\t");
            rTime.Append("count.3");
            rTime.Append("\t");
            rTime.Append("count.4");
            rTime.Append("\t");
            rTime.Append("time.1");
            rTime.Append("\t");
            rTime.Append("time.2");
            rTime.Append("\n");

            for (int i = 0; i < numVals; i++) {
                n = i * jump;

                for (int j = 0; j < jump; j++) {
                    count3 = 0;
                    count4 = 0;
                    a = Generate(n);

                    s.Restart();
                    MinDistance1(a);
                    t1 = s.ElapsedTicks;
                    s.Restart();
                    MinDistance2(a);
                    t2 = s.ElapsedTicks;

                    rTime.Append(n.ToString());
                    rTime.Append("\t");
                    rTime.Append(count3.ToString());
                    rTime.Append("\t");
                    rTime.Append(count4.ToString());
                    rTime.Append("\t");
                    rTime.Append(t1.ToString());
                    rTime.Append("\t");
                    rTime.Append(t2.ToString());
                    rTime.Append("\n");
                }
            }

            File.WriteAllText("D:/Documents/subjects/CAB301/Assignment2/rTime", rTime.ToString());
        }

        static void Test() {
            Dictionary<int[], int> cases = new Dictionary<int[], int> 
            {
                // TEST CASES AND EXPECTED RESULTS HERE
                { new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13  }, 1},
                { new int[] {  }, int.MaxValue},
                { new int[] { 1 }, int.MaxValue},
                { new int[] { 2, 6 }, 4},
                { new int[] { 10, 2 }, 8},
                { new int[] { 2, 2 }, 0},
                { new int[] { 49, 117, 52, 18, 69, 110, 1, 31, 23, 5, 92, 11, 45, 67, 21, 34, 78, 563 }, 2},
                { new int[] { int.MinValue, int.MaxValue }, 1},
                { new int[] { -29, -6, -38 }, 9},
                { new int[] { 1, 1, 1, 1, 1, 1, 1 }, 0},
                { new int[] { -12, -2, 9, 56, 87, -24 }, 10}
            };

            Dictionary<int[], int>.Enumerator e = cases.GetEnumerator();
            
            for (int i = 1; e.MoveNext(); i++) {
                Console.Write("Algorithm 1, Test Case {0} {1}  \t", i, MinDistance1(e.Current.Key) == e.Current.Value ? "passed" : "failed");
                Console.Write("Algorithm 2, Test Case {0} {1}  \n", i, MinDistance2(e.Current.Key) == e.Current.Value ? "passed" : "failed");
            }
            Console.ReadKey();
        }

        static int MinDistance1(int[] a) {
            int d = int.MaxValue;
            int n = a.Length;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (i != j && Math.Abs(a[i] - a[j]) < d) {
                        count3++;
                        d = Math.Abs(a[i] - a[j]);
                    }
                }
            }
            return d;
        }

        static int MinDistance2(int[] a) {
            int d = int.MaxValue;
            int n = a.Length;
            int t;
            for (int i = 0; i < n - 1; i++) {
                for (int j = i + 1; j < n; j++) {
                    t = Math.Abs(a[i] - a[j]);
                    if (t < d) {
                        count4++;
                        d = t;
                    }
                }
            }
            return d;
        }

        static int MinDistance1Count(int[] a) {
            int d = int.MaxValue;
            int n = a.Length;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (i != j && ++count1 > 0 && Math.Abs(a[i] - a[j]) < d) {
                        d = Math.Abs(a[i] - a[j]);
                    }
                }
            }
            return d;
        }

        static int MinDistance2Count(int[] a) {
            int d = int.MaxValue;
            int n = a.Length;
            int t;
            for (int i = 0; i < n - 1; i++) {
                for (int j = i + 1; j < n; j++) {
                    count2++;
                    t = Math.Abs(a[i] - a[j]);
                    if (t < d) {
                        d = t;
                    }
                }
            }
            return d;
        }

        static int[] Generate(int n) {
            int[] a = new int[n];
            for (int i = 0; i < n; i++) {
                a[i] = r.Next();
            }
            return a;
        }
    }
}
