# SPF Graph
Program to Identify Parallelism in the Algorithms by Constructing a Parallel-stacked Form (SPF) Information Graph Algorithm

Technical task: https://drive.google.com/open?id=1vkJvMoRzib138XbuXQKvvHbBjeFhdC4y

Input format description: 
This program uses custom format for storage graph as a file. Example: 

input.txt

3
#
0 -> 1 2
1 -> 2
2 ->
#

The first line of the file should contains one number N - amount of verds in the graph. Second line sign '#'. In the next N rows recorded vertex and it's adjacent vertices, splitted by '->' character. The last row as a second has sign '#'.