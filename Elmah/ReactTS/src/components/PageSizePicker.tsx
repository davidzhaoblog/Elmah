import React from "react";
import { useTranslation } from 'react-i18next';
import { FormControl, FormHelperText, MenuItem, Select, Typography } from "@material-ui/core";

interface IPageSizePickerProps {
    classes: any;
    pageSize: number;
    pageSizes: number[];
    handlePageSizeChange: (event: React.ChangeEvent<{ name?: string; value: unknown }>) => void;
}

export default function PageSizePicker(props: IPageSizePickerProps): JSX.Element {
    const { t } = useTranslation(["UIStringResource"]);

    return (
        <FormControl className={props.classes.formControl}>
            <Select
                className={props.classes.selectEmpty}
                value={props.pageSize}
                name="age"
                onChange={props.handlePageSizeChange}
                inputProps={{ 'aria-label': t('UIStringResource:PageSize') }}
            >
                {props.pageSizes.map((pageSize: any) => {
                    return (
                        <MenuItem key={pageSize} value={pageSize}>
                            <Typography variant="inherit">{pageSize}</Typography>
                            {/* <ListItemIcon>
                                <ArrowUpwardIcon fontSize="small" />
                            </ListItemIcon> */}
                        </MenuItem>                        
                        // <option key={pageSize} value={pageSize} >{pageSize}</option>
                    );
                })}
            </Select>
            <FormHelperText>{t('UIStringResource:PageSize')}</FormHelperText>
        </FormControl>
    );
}
