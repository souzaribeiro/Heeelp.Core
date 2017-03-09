using Heeelp.Core.Domain.ReadModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Common;

namespace Heeelp.Core.Domain.ReadModel.Dao
{
    public class PersonSkinDao : IPersonSkinDao
    {
        private readonly Func<HeeelpReadDataContext> _contextFactory;
        public PersonSkinDao(Func<HeeelpReadDataContext> contextFactory)
        {
            this._contextFactory = contextFactory;
        }
        public Skin Get(Skin t)
        {
            var context = this._contextFactory();
            return context.Skin.Where(x => x.SkinId == t.SkinId).FirstOrDefault();
        }

        public IEnumerable<Skin> List()
        {
            var context = this._contextFactory();
            return context.Skin.Where(x => x.Active).ToList();
        }

        public PersonSkinDTO GetPersonSkin(Guid PersonIntegrationCodeId)
        {
            var context = this._contextFactory();
            var personSkin = (from p in context.Person
                              join s in context.Skin on p.SkinId equals s.SkinId
                              where p.IntegrationCode == PersonIntegrationCodeId
                              select new { p.UrlImageLogo, s.CssFileSkin }).FirstOrDefault();
            if (personSkin != null)
                return new PersonSkinDTO()
                {
                    CssFileSkin = personSkin.CssFileSkin,
                    UrlImageLogo = personSkin.UrlImageLogo
                };
            return null;
        }

        public PersonSkinDTO GetPersonSkin(int UserId)
        {
            var context = this._contextFactory();
            var personSkin = (from p in context.Person
                              join u in context.User on p.PersonId equals u.PersonId
                              join s in context.Skin on p.SkinId equals s.SkinId
                              where u.UserId == UserId
                              select new { p.UrlImageLogo, s.CssFileSkin }).FirstOrDefault();
            if (personSkin != null)
                return new PersonSkinDTO()
                {
                    CssFileSkin = personSkin.CssFileSkin,
                    UrlImageLogo = personSkin.UrlImageLogo
                };
            return null;
        }

        public PersonSkinDTO GetPersonSkinBySubDomain(string subDomain)
        {
            var url = (new Uri(subDomain)).Host.Replace("www.", "");
            var context = this._contextFactory();
            var personSkin = (from p in context.Person
                              join pbc in context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                              join s in context.Skin on p.SkinId equals s.SkinId
                              where pbc.CustomHeeelpPersonDomain == url
                              select new { p.UrlImageLogo, pbc.CustomClubName, pbc.CustomClubLogo, s.CssFileSkin, pbc.CustomImageTheme, pbc.CustomPresentationText, pbc.DiagramView }).FirstOrDefault();
            if (personSkin != null)
                return new PersonSkinDTO()
                {
                    CustomClubName = personSkin.CustomClubName,
                    CssFileSkin = personSkin.CssFileSkin,
                    UrlImageLogo = personSkin.UrlImageLogo,
                    CustomPresentationText = personSkin.CustomPresentationText,
                    CustomImageTheme = personSkin.CustomImageTheme,
                    DiagramView = personSkin.DiagramView,
                    CustomClubLogo = personSkin.CustomClubLogo
                };
            return null;
        }

        public PersonBenefitClubDTO GetPersonBenefitClub(int personId)
        {
            var context = this._contextFactory();
            var ret = (from p in context.Person
                       join pbc in context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                       where p.PersonId == personId
                       select pbc).FirstOrDefault();
            if (ret != null)
                return new PersonBenefitClubDTO() { CustomClubLogo = ret.CustomClubLogo, Description = ret.Description, CustomClubName = ret.CustomClubName, CustomHeeelpPersonDomain = ret.CustomHeeelpPersonDomain };
            return null;
        }

        public PersonBenefitClubDTO GetPersonBenefitClubByDomain(string domain)
        {
            var context = this._contextFactory();
            var ret = (from p in context.Person
                       join pbc in context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                       where pbc.CustomHeeelpPersonDomain == domain
                       select pbc).FirstOrDefault();
            if (ret != null)
                return new PersonBenefitClubDTO() { CustomClubLogo = ret.CustomClubLogo, Description = ret.Description, CustomClubName = ret.CustomClubName, CustomHeeelpPersonDomain = ret.CustomHeeelpPersonDomain };
            else
            {
                return (from p in context.Person
                        join pbc in context.PersonBenefitClub on p.PersonBenefitClubId equals pbc.PersonBenefitClubId
                        where pbc.PersonId == (int)GeneralEnumerators.EnumUserDefault.Heeelp
                        select new PersonBenefitClubDTO()
                        {
                            CustomClubLogo = pbc.CustomClubLogo,
                            Description = pbc.Description,
                            CustomClubName = pbc.CustomClubName,
                            CustomHeeelpPersonDomain = pbc.CustomHeeelpPersonDomain
                        }).FirstOrDefault();
            }
        }
    }
}
