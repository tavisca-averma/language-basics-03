using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        private static List<int> FindSmallest(int[] arr, List<int> list)
        {
            int min = arr[list[0]];
            List<int> updated_index_list = new List<int>();
            foreach (int x in list)
            {
                if (min == arr[x])
                {
                    updated_index_list.Add(x);
                }
                else if (min > arr[x])
                {
                    updated_index_list.Clear();
                    min = arr[x];
                    updated_index_list.Add(x);
                }
                else
                {
                    continue;
                }
            }
            return updated_index_list;
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int len = protein.Length;
            int[] calorie = new int[len];
            int[] meals = new int[dietPlans.Length];
            for( int i = 0; i < len; i++)
            {
                calorie[i] = CalorieCount(protein[i], carbs[i], fat[i]);
            }
            for ( int i = 0; i < dietPlans.Length; i++)
            {
                var index_list = Enumerable.Range(0,len).ToList();
                foreach ( char x in dietPlans[i])
                {
                    bool flag = true;
                    switch (x)
                    {
                        case 'C':
                            index_list = FindLargest(carbs, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'c':
                            index_list = FindSmallest(carbs, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'P':
                            index_list = FindLargest(protein, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'p':
                            index_list = FindSmallest(protein, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'F':
                            index_list = FindLargest(fat, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'f':
                            index_list = FindSmallest(fat, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 'T':
                            index_list = FindLargest(calorie, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        case 't':
                            index_list = FindSmallest(calorie, index_list);
                            if (index_list.Count == 1)
                            {
                                flag = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
                meals[i] = index_list[0];
            }
            
            return meals;
        }

        

        private static List<int> FindLargest(int[] arr, List<int> list)
        {
            int max = arr[list[0]];
            List<int> updated_index_list = new List<int>();
            foreach (int x in list)
            {
                if (max < arr[x])
                {
                    updated_index_list.Clear();
                    max = arr[x];
                    updated_index_list.Add(x);
                }
                else if (max == arr[x])
                {
                    updated_index_list.Add(x);
                }
                else
                {
                    continue;
                }
            }
            return updated_index_list;
        }

        private static int CalorieCount(int carb,int protien,int fat)
        {
            return carb * 5 + pro * 5 + fat * 9;
        }
    }
}
