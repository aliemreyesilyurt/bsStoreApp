﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferOcjects
{
    public record BookDtoForUpdate(int Id, String Title, decimal Price);
}