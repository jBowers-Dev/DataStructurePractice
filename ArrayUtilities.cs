namespace JoshBowersDEV.DataStructurePractice
{
    public static class ArrayUtilities
    {
        /// <summary>
        /// Custom extension for reversing a given array
        /// </summary>
        /// <param name="array">The array to reverse</param>
        /// <returns></returns>
        public static Array Reversal(this Array array)
        {
            Array result = Array.CreateInstance(array.GetType(), array.Length);
            int index = array.Length;
            for (int i = 0; i < index; i++)
            {
                result.SetValue(array.GetValue(array.Length - i), i);
            }

            return result;
        }

        public static int[] BubbleSort(this int[] array)
        {
            int temp;
            bool swapped;

            for (int pass = 0; pass < array.Length - 1; pass++)
            {
                swapped = false;

                for (int i = 0; i < array.Length - pass - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        // Swap elements
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                // If no two elements were swapped in inner loop, the array is already sorted
                if (!swapped)
                    break;
            }

            return array; // Return the sorted array
        }

        public static int[] QuickSort(this int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                int pivotIndex = Partition(array, leftIndex, rightIndex);

                // Recursively sort the two subarrays
                QuickSort(array, leftIndex, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, rightIndex);
            }

            return array;
        }

        private static int Partition(int[] array, int leftIndex, int rightIndex)
        {
            int pivot = array[rightIndex];
            int i = leftIndex - 1;

            for (int j = leftIndex; j < rightIndex; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    // Swap array[i] and array[j]
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            // Swap array[i + 1] and array[rightIndex] (pivot)
            int pivotTemp = array[i + 1];
            array[i + 1] = array[rightIndex];
            array[rightIndex] = pivotTemp;

            return i + 1; // Return the index of the pivot element
        }
    }
}