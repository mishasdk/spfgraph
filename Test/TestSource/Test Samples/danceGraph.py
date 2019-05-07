n = 300

print(n)
print('#')

for i in range(n):
    print(i, end = ' -> ')
    for j in range(i + 1, n):
        print(j, end = ' ')
    print()

print('#')
