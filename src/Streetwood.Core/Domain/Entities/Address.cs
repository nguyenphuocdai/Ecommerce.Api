﻿using System;
using System.Collections.Generic;
using Streetwood.Core.Domain.Abstract;

namespace Streetwood.Core.Domain.Entities
{
    public class Address : Entity
    {
        private readonly IList<Order> orders = new List<Order>();

        public string Street { get; protected set; }

        public string City { get; protected set; }

        public string Country { get; protected set; }

        public string PostCode { get; protected set; }

        public int PhoneNumber { get; protected set; }

        public virtual ICollection<Order> Orders => orders;

        public Address(string street, string city, string country, string postCode, int phoneNumber)
        {
            Id = Guid.NewGuid();
            Street = street;
            City = city;
            Country = country;
            PostCode = postCode;
            PhoneNumber = phoneNumber;
        }

        protected Address()
        {
        }

        public void SetCity(string city)
            => City = city;

        public void SetCountry(string country)
            => Country = country;

        public void SetStreet(string street)
            => Street = street;

        public void SetPostCode(string postCode)
            => PostCode = postCode;
    }
}