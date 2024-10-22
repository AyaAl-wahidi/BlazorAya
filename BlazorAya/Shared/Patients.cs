﻿using LazurdIT.FluentOrm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAya.Shared
{
    [FluentTable("Patients")]
    public class Patients : IFluentModel
    {
        [FluentField(name: "PatientId", autoGenerated: true, isPrimary: true, allowNull: false)]
        public int Id { get; set; }

        [FluentField(name: "PatientName", allowNull: false)]
        public string Name { get; set; } = string.Empty;

        [FluentField(name: "PatientAddress", allowNull: true)]
        public string? Address { get; set; }

        [FluentField(name: "DateOfBirth", allowNull: true)]
        public DateTime? DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"Patient: Id: {Id}, Name: {Name}, Address: {Address}, DateOfBirth: {DateOfBirth?.ToString("yyyy-MM-dd") ?? "N/A"}";
        }
    }
}
