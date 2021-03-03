import React from "react";
import { useTranslation } from 'react-i18next';
import { FormControl, FormHelperText, Select, MenuItem, ListItemIcon, Typography } from "@material-ui/core";
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';
import { QueryOrderBySetting } from "src/framework/Queries/QueryOrderBySetting";

interface IOrderByPickerProps {
    classes: any;
    orderBy: string;
    orderBys: QueryOrderBySetting[];
    handleOrderByChange: (event: React.ChangeEvent<{ name?: string; value: unknown }>) => void;
}

export default function OrderByPicker(props: IOrderByPickerProps): JSX.Element {
    const { t } = useTranslation(["UIStringResource", "UIStringResourcePerEntity"]);
    return (
        <FormControl className={props.classes.formControl}>
            <Select
                className={props.classes.selectEmpty}
                value={props.orderBy}
                name="orderBy"
                onChange={props.handleOrderByChange}
                inputProps={{ 'aria-label': t('UIStringResource:SortBy') }}
            >
                {props.orderBys.map((orderBy) => {
                    return (
                        <MenuItem key={orderBy.expression} value={orderBy.expression}>
                            <Typography variant="inherit">{orderBy.displayName}</Typography>
                            <ListItemIcon className={props.classes.selectEmpty1}>
                                <ArrowUpwardIcon fontSize="small" className={props.classes.selectEmpty1}/>
                            </ListItemIcon>
                        </MenuItem>
                    );
                })}
            </Select>
            <FormHelperText>{t('UIStringResource:SortBy')}</FormHelperText>
        </FormControl>
    );
}
