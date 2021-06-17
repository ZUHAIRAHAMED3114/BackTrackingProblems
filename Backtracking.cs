using System;
using System.Collections.Generic;


namespace RecursionBacktracking
{
    public class BackTracking
    {
        public static int[] x_axis = { 0, 0, 1, 1, 1, -1, -1, -1 };
        public static int[] y_axis = { 1, -1, 0, 1, -1, 0, 1, -1 };
        // problem-1
        public static void RatInMaze(int[,] path){

            bool[,] visited = new bool[path.GetLength(0),path.GetLength(1)];
            int x_start = 0;
            int y_start = 0;
            visited[0,0] = true;
            if (checkPath(path, x_start, y_start, visited, 0)) {
                for (int y = 0; y < visited.GetLength(0); y++) {
                    for (int x=0;x<visited.GetLength(1);x++){
                        if (visited[x, y]) {
                            Console.Write($"{x},{y}--");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        private static bool checkPath(int[,] path, int x_start, int y_start, bool[,] visited, int depth)
        {
            if (x_start ==visited.GetLength(1) - 1 && y_start ==visited.GetLength(0) - 1) { visited[x_start, y_start] = true; return true; }
            if (depth == visited.Length) return false;
            for (int i = 0; i < x_axis.Length; i++) {
                int new_x_position = x_start + x_axis[i];
                int new_y_position = y_start + y_axis[i];
                if (isValid(new_x_position, new_y_position, visited) )
                {
                    if (path[new_x_position, new_y_position] == 1) {
                        visited[new_x_position, new_y_position] = true;
                        if (checkPath(path, new_x_position, new_y_position, visited, depth + 1))
                        {
                            return true;
                        }
                        visited[new_x_position, new_y_position] = false;


                    }

                }
            }

            return false;
        }
        private static bool isValid(int x_currenPosition,int y_position,bool[,] visited){
            if (x_currenPosition >= 0 && x_currenPosition < visited.GetLength(1) && 
                y_position >= 0 && y_position < visited.GetLength(0) &&
                !visited[x_currenPosition, y_position]) 
                return true;
            else return  false;
        }
        // problem-2
        public static void Boogle(char[,] board,List<string> dictionary){
            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];
            
            for (int y=0;y<board.GetLength(0);y++) {
                for (int x=0;x<board.GetLength(1);x++) {
                    visited[y, x] = true;
                    dfs(board, dictionary, board[y, x].ToString(), y, x, 0, visited);
                    visited[y, x] = false; 
             
                }
            }
        }
        private static void dfs(char[,] board, List<string> dictionary, string helper, int y, int x, int depth, bool[,] visited)
        {
            if (dictionary.Contains(helper)) {
                Console.WriteLine(helper);
                return;
             }
            if (depth == visited.Length) return;

            for (int i=0;i<y_axis.Length;i++) {
                int newY = y + y_axis[i];
                int newX = x + x_axis[i];
                if (isValid(newX, newY, visited)) {
                    visited[newY,newX] = true;
                    dfs(board, dictionary, helper + board[newY, newX],newY,newX, depth + 1, visited);
                    visited[newY, newX] = false;
                }
            }

        }
        private static bool isValid(int x_postion,int y_postion,bool[] visited) {
            if (x_postion >= 0 && y_postion >= 0 && x_postion < visited.GetLength(1) && y_postion < visited.GetLength(0)) return true;
            else
                return false;
        }
        // problem-3
        public static List<List<string>> partitonPalindrome(string word) {
            List<List<string>> allpaths = new List<List<string>>();
            int start = 0;
            int end = word.Length;
            for (int k=start;k<end;k++) {
                var currentList = new List<string>();
                if (isPalindrome(word.Substring(start, (k- start + 1)))){
                    currentList.Add(word.Substring(start,k - start + 1));
                    partitonPalindrome(word, k + 1, end, allpaths,currentList,0);
                }
            }

            return allpaths;

        }
        private static void partitonPalindrome(string word, int start, int end,List<List<string>> totalpath ,List<string> currentpath, int depth)
        {
            if (start == end) {
                var newListofstring = new List<string>();
                for (int i = 0; i < currentpath.Count; i++) {
                    newListofstring.Add(currentpath.ElementAt(i)); 
                }
                totalpath.Add(newListofstring);
            }


            for (int k=start;k<end;k++) {
                var currentWord = word.Substring(start, (k - start) + 1);
                if (isPalindrome(currentWord)) {
                    currentpath.Add(currentWord);
                    partitonPalindrome(word,k+1,end,totalpath,currentpath,depth+1);
                    currentpath.Remove(currentWord);
                }

            }
        }
        private static bool isPalindrome(string currentWord)
        {
            int i = 0;
            int j = currentWord.Length - 1;
            while (i <= j) {
                if (currentWord[i] != currentWord[j]) return false;
                i++;
                j--;
            }
            return true;
        }
        // problem-4
        public static bool subsetSum(int[] items, int sum) {
            bool[] visisted = new bool[items.Length];
            
            for (int i=0;i<sum;i++) {
                if (items[i] <= sum) {
                    visisted[i] = true;
                    if (subsetSum(items, visisted, sum,1)) {
                        return true;
                    }
                    visisted[i] = false;
                }
            }
            return false;
        }

        private static bool subsetSum(int[] items, bool[] visisted, int sum,int depth)
        {
            if (sum == 0) return true;
            if (depth == items.Length) return false;

            for (int i=0;i<items.Length;i++) {
                if (isValidSum(items[i],i, sum, visisted)) {
                    visisted[i] = true;
                    if (subsetSum(items, visisted, sum - items[i], depth + 1)) {
                        return true;
                    }
                    visisted[i] = false;
                }
            }
            return false;
        }
        private static bool isValidSum(int currentvalue,int currentPosition ,int sum, bool[] visisted)
        {
            if (currentvalue <= sum && !visisted[currentPosition]) {
                return true;
             }
            return false;
        }
 
     // see remaing problem question from the geeksandgeeks
    }
 

}
