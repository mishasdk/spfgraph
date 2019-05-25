# SPF Graph

Program to Identify Parallelism in the Algorithms by Constructing a Parallel-stacked Form (SPF) Information Graph Algorithm

---
### Introduce

Parallelization of data processing is one of the main ways to improve the performance of computing systems. To identify areas of the algorithm that can be executed independently of each other, use the method of constructing a stacked-parallel form according to its information graph.

In the information graph of the algorithm (IGA) the vertices are the operators corresponding to some action in the execution of the algorithm. By the presence of edges between two vertices, one can judge about the need to execute the first operator before performing the second. Accordingly, in the absence of an arc, both statements are executed independently of each other. The essence of the IGA is in the description of the information dependencies of a specific algorithm.

The stacked-parallel form of a graph (SPF) - is such a form of an IGA, in which operators standing on the same tiers are carried out in parallel.

### Input format description: 
This program uses custom format for storage graph as a file. Example: 

*input.txt*
```
3
#
0 -> 1 2
1 -> 2
2 ->
#
```

The first line of the file should contains single number N - amount of verds in the graph. Second line sign '#'. In the next N rows recorded vertex and it's adjacent vertices, splitted by '->' character. The last row as a second has sign '#'.
