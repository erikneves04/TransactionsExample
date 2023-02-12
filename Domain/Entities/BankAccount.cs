using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities;

public class BankAccount : Entity
{ 
    public decimal Amount { get; set; }
}