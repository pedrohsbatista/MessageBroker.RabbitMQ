﻿using Publisher.Model.Enums;

namespace Publisher.Model.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
