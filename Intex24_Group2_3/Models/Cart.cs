﻿using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Intex24_Group2_3.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>(); // Equals sign means if we haven't built a list, then build one

        public void AddItem(Project p, int quantity)
        {
            // Add the item to the cart first
            CartLine? line = Lines
                .Where(x => x.Project.ProjectId == p.ProjectId)
                .FirstOrDefault(); // Makes sure you only get one

            // Has this item already been added to the cart?
            if(line == null) // Add a new item if line is null
            {
                Lines.Add(new CartLine
                {
                    Project = p,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity; // Update the count
            }
        }

        //
        public void RemoveLine(Project p) => Lines.RemoveAll(x => x.Project.ProjectId == p.ProjectId);

        public void Clear() => Lines.Clear();

        public decimal CalculateTotal() => Lines.Sum(x => 25 * x.Quantity); // Use Lambda function to get total

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Project Project { get; set; }
            public int Quantity { get; set; }
        }
    }
}
