using System;

namespace LMS;

class Student
{
    public int IndexNumber { get; set; }
    public string Name { get; set; }
    public double GPA { get; set; }
    public int AdmissionYear { get; set; }
    public string NIC { get; set; }

    public Student(int index, string name, double gpa, int year, string nic)
    {
        IndexNumber = index;
        Name = name;
        GPA = gpa;
        AdmissionYear = year;
        NIC = nic;
    }

    public void Display()
    {
        Console.WriteLine($"Index: {IndexNumber}, Name: {Name}, GPA: {GPA}, Year: {AdmissionYear}, NIC: {NIC}");
    }
}

class Node
{
    public Student Data;
    public Node Next;

    public Node(Student student)
    {
        Data = student;
        Next = null;
    }
}

class StudentLinkedList
{
    private Node head;
    private Node tail;

    internal Node Head { get => head; set => head = value; }
    internal Node Tail { get => tail; set => tail = value; }

    public void Insert(Student student)
    {
        if (Search(student.IndexNumber) != null)
        {
            Console.WriteLine("Student with this index number already exists.");
            return;
        }

        Node checkNIC = head;
        while (checkNIC != null)
        {
            if (checkNIC.Data.NIC == student.NIC)
            {
                Console.WriteLine("A student with this NIC already exists.");
                return;
            }
            checkNIC = checkNIC.Next;
        }

        Node newNode = new Node(student);
        if (head == null || student.IndexNumber < head.Data.IndexNumber)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null && current.Next.Data.IndexNumber < student.IndexNumber)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }

        Console.WriteLine("Student inserted successfully.");
    }

    public Student Search(int index)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.IndexNumber == index)
                return current.Data;
            current = current.Next;
        }
        return null;
    }

    public void Remove(int index)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty.");
            return;
        }

        if (head.Data.IndexNumber == index)
        {
            head = head.Next;
            Console.WriteLine("Student removed successfully.");
            return;
        }

        Node current = head;
        while (current.Next != null && current.Next.Data.IndexNumber != index)
        {
            current = current.Next;
        }

        if (current.Next == null)
        {
            Console.WriteLine("Student not found.");
        }
        else
        {
            current.Next = current.Next.Next;
            Console.WriteLine("Student removed successfully.");
        }
    }

    public void PrintAll()
    {
        if (head == null)
        {
            Console.WriteLine("No students found.");
            return;
        }

        Console.WriteLine("\nAll Students:");
        Node current = head;
        while (current != null)
        {
            current.Data.Display();
            current = current.Next;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        StudentLinkedList studentList = new StudentLinkedList();
        int choice;

        do
        {
            Console.WriteLine("\n~Student Management System~");
            Console.WriteLine("1. Insert Student");
            Console.WriteLine("2. Search Student by Index");
            Console.WriteLine("3. Remove Student by Index");
            Console.WriteLine("4. Print All Students");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Last 3 Digits of Index Number (e.g., 123): ");
                    int lastDigits = int.Parse(Console.ReadLine());
                    int index = 2025000 + lastDigits;

                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter GPA: ");
                    double gpa = double.Parse(Console.ReadLine());

                    Console.Write("Enter Admission Year: ");
                    int year = int.Parse(Console.ReadLine());

                    Console.Write("Enter NIC: ");
                    string nic = Console.ReadLine();

                    Student newStudent = new Student(index, name, gpa, year, nic);
                    studentList.Insert(newStudent);
                    break;

                case 2:
                    Console.Write("Enter Index Number to Search: ");
                    int searchIndex = int.Parse(Console.ReadLine());
                    Student found = studentList.Search(searchIndex);
                    if (found != null)
                        found.Display();
                    else
                        Console.WriteLine("Student not found.");
                    break;

                case 3:
                    Console.Write("Enter Index Number to Remove: ");
                    int removeIndex = int.Parse(Console.ReadLine());
                    studentList.Remove(removeIndex);
                    break;

                case 4:
                    studentList.PrintAll();
                    break;

                case 5:
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (choice != 5);
    }
}


