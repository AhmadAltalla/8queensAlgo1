using System;

namespace EightQueensVisualizer
{
    class Program
    {
        static int[] x = new int[8];
        static char[] a = new char[8];
        static char[] b = new char[15];
        static char[] c = new char[15];
        static int solutionsCount = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. إيجاد الحل الأول فقط باستخدام Try1
            ResetArrays();
            char q = 'n';
            Try1(0, ref q);
            if (q == 'y')
            {
                Console.WriteLine("=== الحل الأول (Try1) ===");
                PrintBoard();
            }

            // 2. إيجاد جميع الحلول الـ 92 باستخدام Try2
            ResetArrays();
            solutionsCount = 0;
            Try2(0);
            Console.WriteLine($"\nإجمالي الحلول الممكنة (Try2): {solutionsCount} حلاً.");
        }

        static void ResetArrays()
        {
            Array.Fill(x, -1);
            Array.Fill(a, 'y');
            Array.Fill(b, 'y');
            Array.Fill(c, 'y');
        }

        // الوضع الأول: يتوقف فور إيجاد أول حل صحيح عبر تمرير q بالمرجع
        static void Try1(int i, ref char q)
        {
            int j = -1;
            do
            {
                j++;
                q = 'n';

                // فحص الأمان بتعقيد O(1): الصف، القطر الرئيسي، القطر الثانوي
                if ((a[j] == 'y') && (b[7 + i - j] == 'y') && (c[i + j] == 'y'))
                {
                    x[i] = j;
                    a[j] = 'n';
                    b[7 + i - j] = 'n';
                    c[i + j] = 'n';

                    if (i < 7)
                    {
                        Try1(i + 1, ref q);
                        if (q == 'n')
                        {
                            // التراجع (Backtrack) وتحرير المصفوفات
                            x[i] = -1;
                            a[j] = 'y';
                            b[7 + i - j] = 'y';
                            c[i + j] = 'y';
                        }
                    }
                    else
                    {
                        q = 'y'; // اكتمل وضع الوزراء الثمانية
                    }
                }
            } while ((q == 'n') && (j < 7));
        }

        // الوضع الثاني: يستكشف جميع الحلول الـ 92 الممكنة
        static void Try2(int i)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((a[j] == 'y') && (b[7 + i - j] == 'y') && (c[i + j] == 'y'))
                {
                    x[i] = j;
                    a[j] = 'n';
                    b[7 + i - j] = 'n';
                    c[i + j] = 'n';

                    if (i < 7)
                    {
                        Try2(i + 1);
                    }
                    else
                    {
                        solutionsCount++;
                    }

                    // التراجع الدائم للبحث عن بقية الحلول
                    x[i] = -1;
                    a[j] = 'y';
                    b[7 + i - j] = 'y';
                    c[i + j] = 'y';
                }
            }
        }

        static void PrintBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(x[col] == row ? " Q " : " . ");
                }
                Console.WriteLine();
            }
        }
    }
}