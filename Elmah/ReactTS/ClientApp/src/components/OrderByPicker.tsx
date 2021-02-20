import React from "react";
import { FormControl, FormHelperText, NativeSelect } from "@material-ui/core";
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";

interface IOrderByPickerProps {
    classes: any;
    orderBy: string;
    orderBys: QueryOrderBySetting[];
    handleOrderByChange: (event: React.ChangeEvent<{ name?: string; value: unknown }>) => void;
}

export default function OrderByPicker(props: IOrderByPickerProps): JSX.Element {
    return (
        <FormControl className={props.classes.formControl}>
            <NativeSelect
                className={props.classes.selectEmpty}
                value={props.orderBy}
                name="age"
                onChange={props.handleOrderByChange}
                inputProps={{ 'aria-label': 'Order By' }}
            >
			{props.orderBys.map((orderBy) => {
                return (
                    <option key={orderBy.displayName} value={orderBy.displayName} >{orderBy.displayName}</option>
                );
            })}
            </NativeSelect>
            <FormHelperText>Order By</FormHelperText>
        </FormControl>
    );
}
