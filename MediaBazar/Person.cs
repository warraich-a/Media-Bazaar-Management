﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBazar
{
    public class Person
    {
        private int id;
        protected string firstName;
        protected string lastName;
        protected string email;
        protected string password;
        protected DateTime dateOfBirth;
        protected string streetName;
        protected int houseNr;
        protected string zipcode;
        protected string city;
        protected double hourlyWage;
        protected Roles role;
        protected int departmentId;

        public Person()
        {

        }
        public Person(int givenId, string givenFirstName, string givenSecondName)
        {
            id = givenId;
            FirstName = givenFirstName;
            LastName = givenSecondName;
        }
        public Person(int givenId, string givenFirstName, string givenSecondName, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, Roles givenRole)
        {
            id = givenId;
            FirstName = givenFirstName;
            LastName = givenSecondName;
            Email(givenFirstName, givenSecondName); // creating an email
            dateOfBirth = givenDOB;
            StreetName = givenStreetName;
            HouseNr = givenHouseNr;
            Zipcode = givenZipcode;
            hourlyWage = givenHourlyWage;
            City = givenCity;
            role = givenRole;
        }
        public Person(int givenId, string givenFirstName, string givenSecondName, int givenDId, DateTime givenDOB, string givenStreetName, int givenHouseNr, string givenZipcode, string givenCity, double givenHourlyWage, Roles givenRole)
        {
            id = givenId;
            FirstName = givenFirstName;
            LastName = givenSecondName;
            DepartmentId = givenDId;
            Email(givenFirstName, givenSecondName); // creating an email
            dateOfBirth = givenDOB;
            StreetName = givenStreetName;
            HouseNr = givenHouseNr;
            Zipcode = givenZipcode;
            hourlyWage = givenHourlyWage;
            City = givenCity;
            role = givenRole;
        }

        //first Name
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (value != "")
                {
                    this.firstName = value;
                }
            }
        }


        //id
        public int Id { get { return this.id; } }
        //second name
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (value != "")
                {
                    this.lastName = value;
                }
            }
        }
        // street Name
        public string StreetName
        {
            get
            {
                return this.streetName;
            }
            set
            {
                if (value != "")
                {
                    this.streetName = value;
                }
            }
        }

        //house Number
        public int HouseNr
        {
            get
            {
                return this.houseNr;
            }
            set
            {
                if (value != 0)
                {
                    this.houseNr = value;
                }
            }
        }

        //zipcode
        public string Zipcode
        {
            get
            {
                return this.zipcode;
            }
            set
            {
                if (value != "")
                {
                    this.zipcode = value;
                }
            }
        }

        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                if (value != "")
                {
                    this.city = value;
                }
            }
        }
        public int DepartmentId
        {
            get
            {
                return this.departmentId;
            }
            set
            {
                if (value != 0)
                {
                    this.departmentId = value;
                }
            }
        }

        // date of birth 
        public DateTime DateOfBirth { get { return this.dateOfBirth; } set { dateOfBirth = value; } }
        // to get the email
        public string GetEmail { get { return email; } }
        // email 


        public string Email(string firstName, string lastName)
        {
            return email = $"{firstName + lastName}@mediabazaar.com";

        }

        //to get the role
        public Roles Role { get { return this.role; } set { role = value; } }

        // to get the Hourly Wage
        public double HourlyWage { get { return this.hourlyWage; } set { hourlyWage = value; } }

        // to auto generate a password
        public string SetPassword()
        {
            int minLength = 8;
            int maxLength = 12;

            string charAvailable = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ0123456789";
            StringBuilder newPassword = new StringBuilder();
            Random rnd = new Random();

            int passwordLength = rnd.Next(minLength, maxLength + 1);
            while (passwordLength-- > 0)
            {
                newPassword.Append(charAvailable[rnd.Next(charAvailable.Length)]);
            }

            password = newPassword.ToString();
            return password;
        }
        public override string ToString()
        {
            return $"ID: {id}\r\nName:{firstName} {lastName}\r\nEmail: {email}\r\nBirthdate {dateOfBirth}\r\nADDRESS\r\nStreet {streetName} {houseNr} {zipcode} {city}\r\nHourly Wage:{hourlyWage} \r\nRole: {role}";
        }
        public string GetFullName()
        {
            return this.firstName + " " + this.lastName;
        }
    }
}
