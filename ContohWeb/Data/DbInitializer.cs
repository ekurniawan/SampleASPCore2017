using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ContohWeb.Models;


namespace ContohWeb.Data
{
    public static class DbInitializer
    {
        //untuk menambahkan data seeding / data diawal ketika database dibuat
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
               new Student {FirstMidName="Erick",LastName="Kurniawan",EnrollmentDate=DateTime.Parse("2016-09-01")},
               new Student {FirstMidName="Budi",LastName="Halim",EnrollmentDate=DateTime.Parse("2015-08-01")},
               new Student {FirstMidName="Djoni",LastName="Dwijana",EnrollmentDate=DateTime.Parse("2014-03-01")},
               new Student {FirstMidName="Joko",LastName="Purwanto",EnrollmentDate=DateTime.Parse("2014-03-01")},
               new Student {FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
               new Student {FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
               new Student {FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
               new Student {FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
               new Student {FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")}
            };

            foreach (var student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();


            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Kim",LastName = "Abercrombie",HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Jon",LastName = "Snow",HireDate = DateTime.Parse("1996-03-11") },
                new Instructor { FirstMidName = "Tyrion",LastName = "Lannister",HireDate = DateTime.Parse("2001-03-11") },
                new Instructor { FirstMidName = "Jammie",LastName = "Lannis",HireDate = DateTime.Parse("2003-03-11") },
                new Instructor { FirstMidName = "Robb",LastName = "Starkie",HireDate = DateTime.Parse("2004-03-11") },
                new Instructor { FirstMidName = "Arya",LastName = "Stark",HireDate = DateTime.Parse("2007-03-11") }
            };

            foreach (var instructor in instructors)
            {
                context.Instructors.Add(instructor);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name="English",Budget=350000,StartDate=DateTime.Parse("2007-09-01"),
                 InstructorID=instructors.Single(i=>i.LastName=="Abercrombie").InstructorID},
                new Department { Name="Information Technology",Budget=650000,StartDate=DateTime.Parse("2008-09-01"),
                 InstructorID=instructors.Single(i=>i.LastName=="Snow").InstructorID},
                new Department { Name="Engineering",Budget=350000,StartDate=DateTime.Parse("2007-09-01"),
                 InstructorID=instructors.Single(i=>i.LastName=="Lannister").InstructorID},
                new Department { Name="Business",Budget=350000,StartDate=DateTime.Parse("2007-09-01"),
                 InstructorID=instructors.Single(i=>i.LastName=="Stark").InstructorID},
                new Department { Name="Mathematics",Budget=350000,StartDate=DateTime.Parse("2007-09-01"),
                 InstructorID=instructors.Single(i=>i.LastName=="Starkie").InstructorID}
            };

            foreach (var department in departments)
            {
                context.Departments.Add(department);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Algoritma dan Pemrograman",Credits=3},
                new Course{CourseID=4022,Title="Pemrograman Web",Credits=3},
                new Course{CourseID=4041,Title="Pemrograman Mobile",Credits=3},
                new Course{CourseID=1045,Title="Rekayasa Perangkat Lunak",Credits=3},
                new Course{CourseID=3141,Title="Basis Data",Credits=4},
                new Course{CourseID=2021,Title="Machine Learning",Credits=3},
                new Course{CourseID=2042,Title="Struktur Data",Credits=4}
            };

            foreach (var course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();


            var officeassignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Abercrombie").InstructorID,
                    Location = "Gedung Agape lt 3" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Snow").InstructorID,
                    Location = "Gedung Biblos lt 2" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Stark").InstructorID,
                    Location = "Gedung Agape lt 5" }
            };

            foreach (var officeassignment in officeassignments)
            {
                context.OfficeAssignments.Add(officeassignment);
            }
            context.SaveChanges();

            var courseassigments = new CourseAssignment[]
            {
                 new CourseAssignment { CourseID = courses.Single(c => c.Title == "Algoritma dan Pemrograman" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").InstructorID},
                 new CourseAssignment { CourseID = courses.Single(c => c.Title == "Pemrograman Web" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Snow").InstructorID},
                 new CourseAssignment { CourseID = courses.Single(c => c.Title == "Pemrograman Mobile" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Stark").InstructorID},
                 new CourseAssignment { CourseID = courses.Single(c => c.Title == "Rekayasa Perangkat Lunak" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").InstructorID},
                 new CourseAssignment { CourseID = courses.Single(c => c.Title == "Machine Learning" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Lannister").InstructorID}
            };

            foreach(var courseassignment in courseassigments)
            {
                context.CourseAssignments.Add(courseassignment);
            }
            context.SaveChanges();

            var enrolments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A}
            };

            foreach (var enrollment in enrolments)
            {
                context.Enrollments.Add(enrollment);
            }

            context.SaveChanges();
        }
    }
}
