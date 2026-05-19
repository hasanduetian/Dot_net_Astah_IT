// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Runtime.CompilerServices; // libary of list

// namespace HelloWorld
// {

//     class Car
//     {
//         public string Brand; // field
//         public int Year; // field

//         public Car() // default constructor
//         {
//             Brand="Unknown";
//             Year=0;
//         }
//         public Car(string Brand,int Year)// paramiterwise constructor
//         {
//             this.Brand=Brand; // same name thakle this use kora hoy
//             this.Year=Year;
//         }
//         public Car(Car other) // copy constructor--> it take object in a class
//         {
//             Brand=other.Brand;
//             Year=other.Year;
//         }
//     }
//     class Program
//     {
//         public int numbers;  // fields--> without attribute 
//         public int number{get; private set;}// properties --> auto implemented properties
//        public static int Add(int a,int b)
//         {
//         return a+b;
//         }
//         // constructor-------------
//         static void Main()
//         {
            

//             // b = 5; // it shows error because const value cannot be changed

//             // Console.WriteLine("hello world");

//             // // camelCase for variable --> lastName
//             // // PascalCase for method name --> LastName

//             // //implicit conversion -> no data loss
//             // int num = 11;
//             // double bigName = num;
//             // Console.WriteLine(bigName);

//             // // explicit conversion -> possible data loss
//             // double c = 10.13;
//             // int d = (int)c;   // casting
//             // Console.WriteLine(d);

//             // // convert string
//             // string tmp="123";
//             // int res=Convert.ToInt32(tmp);
//             // Console.WriteLine(res);
//             // Console.WriteLine(res.GetType());

//             // // operator same as c++
//             // // condition same as c++
//             // // switch same as c++
//             // int age =20;
//             // switch (age)
//             // {
//             // case 20:
//             //     Console.WriteLine("hasan");
//             //     break;                
//             // case 15:
//             //     Console.WriteLine("roksana");
//             //     break;                
//             // case 10:
//             //     Console.WriteLine("mahamud");
//             //     break;                
//             // case 7:
//             //     Console.WriteLine("rusha");
//             //     break;
//             // default:
//             //     Console.WriteLine("undefine");
//             //     break;
//             // }

//             // // all loop same as c++

//             // // array 
//             // int[] arr= new int [3]; // array declaration
//             // arr[0]=1;
//             // arr[1]=1;
//             // arr[2]=1;

//             // int [] nums={10,11,12};
//             // Console.WriteLine(nums[2]);

//             // // list --> dynamic type data structure
//             // List<string>box=new List<string>();
//             // box.Add("hasan");
//             // box.Add("roksana");
//             // box.Remove("hasan");
//             // Console.WriteLine(box[2]);

//             //function call
//             int sum=Add(5,4);
//             Console.WriteLine(sum);

//             // Access modifier

//             // value type and reference type
//             int[] ar={1,2,3};
//             int [] ar2 = ar;
//             ar[2]=55;
//             Console.WriteLine(ar[2]);  // 55

//             // constructor
//             Car c1=new Car();
//             Console.WriteLine(c1.Brand);
//             Console.WriteLine(c1.Year);

//             Car c2=new Car("Marcedice",2026);
//             Console.WriteLine(c2.Brand);
//             Console.WriteLine(c2.Year);

//             Car c3=new Car(c2);
//             Console.WriteLine(c3.Brand);
//             Console.WriteLine(c3.Year);



//         }
//     }

// }