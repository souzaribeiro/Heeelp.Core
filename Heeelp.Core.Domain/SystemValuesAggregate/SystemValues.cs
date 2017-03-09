namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class SystemValues
    {

        public SystemValues()
        {
            AddressType = new HashSet<AddressType>();
            ApprovalStatus = new HashSet<ApprovalStatus>();
            Contract = new HashSet<Contract>();
            ContractType = new HashSet<ContractType>();
            Country = new HashSet<Country>();
            Currency = new HashSet<Currency>();
            DocumentType = new HashSet<DocumentType>();
            Expertise = new HashSet<Expertise>();
            Language = new HashSet<Language>();
            Menu = new HashSet<Menu>();
            Module = new HashSet<Module>();
            PageType = new HashSet<PageType>();
            PersonOriginType = new HashSet<PersonOriginType>();
            PersonStatus = new HashSet<PersonStatus>();
            PersonType = new HashSet<PersonType>();
            SystemParameter = new HashSet<SystemParameter>();
            UserProfile = new HashSet<UserProfile>();
            UserStatus = new HashSet<UserStatus>();
        }

        public virtual ICollection<AddressType> AddressType { get; set; }
        public virtual ICollection<ApprovalStatus> ApprovalStatus { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<ContractType> ContractType { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<Currency> Currency { get; set; }
        public virtual ICollection<DocumentType> DocumentType { get; set; }
        public virtual ICollection<Expertise> Expertise { get; set; }
        public virtual ICollection<Language> Language { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<Module> Module { get; set; }
        public virtual ICollection<Neighbourhood> Neighbourhood { get; set; }
        public virtual ICollection<PageType> PageType { get; set; }
        public virtual ICollection<PersonOriginType> PersonOriginType { get; set; }
        public virtual ICollection<PersonStatus> PersonStatus { get; set; }
        public virtual ICollection<PersonType> PersonType { get; set; }
        public virtual ICollection<SystemParameter> SystemParameter { get; set; }
        public virtual ICollection<UserProfile> UserProfile { get; set; }
        public virtual ICollection<UserStatus> UserStatus { get; set; }
    }
}



