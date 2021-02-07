import { FormControl, FormHelperText, NativeSelect } from "@material-ui/core";
import React from "react";

interface IPageSizePickerProps {
    classes: any;
    pageSize: number;
    pageSizes: number[];
    handlePageSizeChange: (event: React.ChangeEvent<{ name?: string; value: unknown }>) => void;
}

export default function PageSizePicker(props: IPageSizePickerProps): JSX.Element {
    return (
        <FormControl className={props.classes.formControl}>
            <NativeSelect
                className={props.classes.selectEmpty}
                value={props.pageSize}
                name="age"
                onChange={props.handlePageSizeChange}
                inputProps={{ 'aria-label': 'Page Size' }}
            >
                {props.pageSizes.map((pageSize: any) => {
                    return (
                        <option key={pageSize} value={pageSize} >{pageSize}</option>
                    );
                })}
            </NativeSelect>
            <FormHelperText>Page Size</FormHelperText>
        </FormControl>
    );
}
