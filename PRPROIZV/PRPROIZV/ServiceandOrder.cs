//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRPROIZV
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceandOrder
    {
        public string ID_Order { get; set; }
        public System.DateTime DateCreate { get; set; }
        public int ID_Client { get; set; }
        public Nullable<int> ID_Service { get; set; }
        public int NumberOrder { get; set; }
        public int ID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Order Order { get; set; }
        public virtual Service Service { get; set; }
    }
}
