using System;

namespace DesignPatterns_FacetedBuilder {

    public class Person {
        //address
        public string StreetAddress, Postcode, City;
        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString() {
            return $"{nameof(StreetAddress)}: {StreetAddress} - {Postcode} - {City} - {CompanyName} - {Position} - {AnnualIncome}";
        }
    }

    // API for building up the address, employment information.
    public class PersonBuilder { // facade. keeps reference to person thats building up and access to sub-builders
        //reference object
        protected Person person = new Person();

        //Actual API
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb) {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder {
        public PersonAddressBuilder(Person person) {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress) {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode) {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city) {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder {
        public PersonJobBuilder(Person person) {
            this.person = person;
        }

        // Fluent API
        public PersonJobBuilder At(string companyName) {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position) {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount) {
            person.AnnualIncome = amount;
            return this;
        }
    }

    internal class Program {
        static void Main(string[] args) {
            var pb = new PersonBuilder();
            Person person = pb.Lives.At("123 Main St").In("Phoenix").WithPostcode("49910")
                            .Works.At("SpaceX").AsA("Developer").Earning(123000);

            Console.WriteLine(person);
        }
    }
}
