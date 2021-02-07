import * as React from 'react'
import { Pagination } from '@material-ui/lab';
import { IListProps } from 'src/framework/ViewModels/IListProps';
import ListItem from './ListItem';

export default function List(props: IListProps) {
    const classes = props.classes;

    return (
        <div className={classes.root}>
            <Pagination
                className="my-3"
                count={props.count}
                page={props.page}
                siblingCount={1}
                boundaryCount={1}
                variant="outlined"
                shape="rounded"
                onChange={props.handlePageChange}
            />
            {props.items.map((item: any) => {
                return (
                    <ListItem key={item.id} item={item} classes={props.classes} />
                );
            })}
        </div>
    );
}
