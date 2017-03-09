﻿using Heeelp.Core.Domain.ReadModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain.ReadModel.Interfaces
{
    public interface IPersonSkinDao : IReadBase<Skin>
    {
        PersonSkinDTO GetPersonSkin(Guid personIntegrationCode);

        PersonSkinDTO GetPersonSkin(int  UserId);

        PersonSkinDTO GetPersonSkinBySubDomain(string subDomain);

        PersonBenefitClubDTO GetPersonBenefitClub(int personId);

        PersonBenefitClubDTO GetPersonBenefitClubByDomain(string domain);

    }
}
