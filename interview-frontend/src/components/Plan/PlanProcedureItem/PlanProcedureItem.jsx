import React from "react";
import ReactSelect from "react-select";
const PlanProcedureItem = ({ procedure, users,handleAssignUserToProcedure,planProceduresUsers }) => {
    // const handleAssignUserToProcedure = (e) => {
    //     setSelectedUsers(e);
    //     // TODO: Remove console.log and add missing logic
    //     console.log(e);
    // };
    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                defaultValue={users.map(
                    ({ value, label }, index)=>{
                        if(planProceduresUsers.find(
                            (p) => p.userId === value && p.procedureId===procedure.procedureId
                        )){
                        return users[index];
                        }
                    })
                }
                onChange={(e,selectValue) => handleAssignUserToProcedure(selectValue.option.value,procedure)}
            />
        </div>
    );
};

export default PlanProcedureItem;
