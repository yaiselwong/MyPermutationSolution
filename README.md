This is the solution for the problem

A permutation of a vector of integers is an arrangement of its members into a sequence or linear order.
● For example, for vec = [1,2,3], the following are considered permutations of vec: [1,2,3], [1,3,2], [3,1,2], [2,3,1].
The next permutation of a vector of integers is the next lexicographically greater permutation of its integers. More
formally, if all the permutations of the vector are sorted in one container according to their lexicographical order, then
the next permutation of that vector is the permutation that follows it in the sorted container. If such an arrangement is
not possible, the vector must be rearranged as the lowest possible order (i.e., sorted in ascending order).
● For example, the next permutation of vec = [1,2,3] is [1,3,2].
● Similarly, the next permutation of vec = [2,3,1] is [3,1,2].
● While the next permutation of vec = [3,2,1] is [1,2,3] because [3,2,1] does not have a lexicographical larger
rearrangement.
A. Given a vector of integers nums, find the next permutation of nums.
B. The replacement must be in place and use only constant extra memory.
C. Provide the necessary unit tests.
Example 1:
Input: nums = [1,2,3]
Output: [1,3,2]
Example 2:
Input: nums = [3,2,1]
Output: [1,2,3]
Example 3:
Input: nums = [1,1,5]
Output: [1,5,1]

The solution was created using a Blazor Webassambly app with an ASP .NET Api.

A github action to compile and automatic deploy after the master branch recieve a change.
The temporal url for this server is https://yaiselwong-001-site1.ctempurl.com/
