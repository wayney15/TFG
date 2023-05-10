﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Repositories
{
    public interface ILandBidRepository
    {
        Task<LandBid> CreateLandAsync(LandBid landBid);
    }
}