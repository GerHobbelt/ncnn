// Tencent is pleased to support the open source community by making ncnn available.
//
// Copyright (C) 2022 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the BSD 3-Clause License (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at
//
// https://opensource.org/licenses/BSD-3-Clause
//
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.

#include "reset_device.h"

namespace pnnx {

void reset_device(std::shared_ptr<torch::jit::Graph>& graph, const std::string& device)
{
    for (torch::jit::Node* n : graph->nodes())
    {
        if (n->kind().is_aten())
        {
            if (n->hasNamedInput("dtype"))
            {
                torch::jit::Node* dtype_node = n->namedInput("dtype")->node();

                if (dtype_node->hasAttribute(torch::jit::attr::value))
                {
                    // change dtype=half to dtype=float
                    if (dtype_node->kindOf(torch::jit::attr::value) == torch::jit::AttributeKind::i && dtype_node->i(torch::jit::attr::value) == 5)
                    {
                        dtype_node->i_(torch::jit::attr::value, 6);
                    }
                    // change dtype=bfloat16 to dtype=float
                    if (dtype_node->kindOf(torch::jit::attr::value) == torch::jit::AttributeKind::i && dtype_node->i(torch::jit::attr::value) == 15)
                    {
                        dtype_node->i_(torch::jit::attr::value, 6);
                    }
                }
            }

            if (n->hasNamedInput("device"))
            {
                torch::jit::Node* device_node = n->namedInput("device")->node();

                device_node->s_(torch::jit::attr::value, (device == "gpu") ? "cuda" : "cpu");
            }
        }
    }
}

} // namespace pnnx
