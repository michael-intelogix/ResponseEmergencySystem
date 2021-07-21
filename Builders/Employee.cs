using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Builders
{
    public sealed class Employee
    {
        public Guid ID { get; set; } 
        public Guid ID_General { get; set; }
        public Guid ID_Employee { get; set; }
        public string ID_Samsara { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string PhoneNumber { get; set; }
        public string License { get; set; }
        public string State { get; set; }
        public string ID_StateOfExpedition { get; set; }
        public string ExpirationDate { get; set; }
        public bool Exists { get; set; } = false;
        public bool IsLocal { get; set; } = false;
        public bool IsSamsara { get; set; } = false;
        public string Status { get; private set; }

        public Employee()
        {
            if(IsSamsara && !Exists)
                SetNewEmployee();
        }

        public bool ValidatePhoneNumber(string newPhoneNumber)
        {
            string newValue = newPhoneNumber.Trim();

            if (this.PhoneNumber != newValue && newValue != "")
            {
                this.PhoneNumber = newValue;
                if (this.Exists)
                    this.Status = "updated";
                else
                    this.Status = "added";

                return true;
            }

            return false;
        }

        public bool ValidateLicense(string newLicense)
        {
            string newValue = newLicense.Trim();

            if (this.License != newValue && newValue != "")
            {
                this.License = newValue;
                if (this.Exists)
                    this.Status = "updated";
                else
                    this.Status = "added";

                return true;
            }

            return false;
        }

        public void SetNewEmployee()
        {
            Status = "added";
        }
    }

    public abstract class FunctionalEmployeeBuilder<TSubject, TSelf>
    where TSelf : FunctionalEmployeeBuilder<TSubject, TSelf>
    where TSubject : new()
    {
        private readonly List<Func<Employee, Employee>> actions = new List<Func<Employee, Employee>>();

        public TSelf Do(Action<Employee> action) => AddAction(action);

        public Employee Build() => actions.Aggregate(new Employee(), (e, f) => f(e));

        private TSelf AddAction(Action<Employee> action)
        {
            actions.Add(e =>
            {
                action(e);
                return e;
            });

            return (TSelf)this;
        }
    }

    public sealed class EmployeeBuilder : FunctionalEmployeeBuilder<Employee, EmployeeBuilder>
    {
        
        public EmployeeBuilder SetNewID() => Do(e => e.ID = Guid.NewGuid());
        public EmployeeBuilder Called(string name) => Do(p => p.Name = name);
        public EmployeeBuilder PhoneNumber(string phoneNumber) => Do(e => {
            e.PhoneNumber = phoneNumber;
        });
        public EmployeeBuilder LicenseNumber(string license) => Do(p => p.License = license);
        public EmployeeBuilder State(string state) => Do(p => p.State = state);
        public EmployeeBuilder IsRegisteredInGeneral(Guid generalID, string samsaraID) => Do(e => {
            e.ID_General = generalID;
            e.ID_Samsara = samsaraID;

            if (generalID == Guid.Empty)
                e.IsSamsara = true;
            else
                e.IsLocal = true;
        });
        public EmployeeBuilder HasEmployeeID(Guid employeeID) => Do(e => {
            e.ID_Employee = employeeID;
            if(employeeID != Guid.Empty)
                e.Exists = true;
        });

        public EmployeeBuilder NewEmployee() => Do(e => {
            e.SetNewEmployee();
        });
    }

    //public static class EmployeeBuilderExtensions
    //{
    //    public static EmployeeBuilder WorkAs(this EmployeeBuilder builder, string position) => builder.Do(p => p.Position = position);
    //}
}
