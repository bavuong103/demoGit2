using System.Diagnostics.CodeAnalysis;

namespace QuanLyNhanVienDbFirst.Models
{
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            //return x.Salary == y.Salary && x.Name.ToLower() == y.Name.ToLower();
            return x.Name == y.Name;

            /*if (x.Id == y.Id)
                return true;

            return false;*/
        }

        public int GetHashCode(Employee obj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(obj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            //Get hash code for the Code field.
            //int hashProductSalary = obj.Salary == null ? 0 : obj.Salary.GetHashCode();

            //Calculate the hash code for the product.
            //return hashProductName ^ hashProductSalary;
            return hashProductName;
        }
    }
}
