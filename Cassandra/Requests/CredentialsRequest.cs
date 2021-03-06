//
//      Copyright (C) 2012 DataStax Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
﻿using System.Collections.Generic;

namespace Cassandra
{
    internal class CredentialsRequest : IRequest
    {
        public const byte OpCode = 0x04;
        readonly int _streamId;
        readonly IDictionary<string, string> _credentials;
        public CredentialsRequest(int streamId, IDictionary<string, string> credentials)
        {
            this._streamId = streamId;
            this._credentials = credentials;
        }
        public RequestFrame GetFrame()
        {
            var wb = new BEBinaryWriter();
            wb.WriteFrameHeader(0x01, 0x00, (byte)_streamId, OpCode);
            wb.WriteUInt16((ushort)_credentials.Count);
            foreach (var kv in _credentials)
            {
                wb.WriteString(kv.Key);
                wb.WriteString(kv.Value);
            }
            return wb.GetFrame();
        }
    }
}
