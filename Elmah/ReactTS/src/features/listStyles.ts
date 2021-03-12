import { makeStyles, Theme } from '@material-ui/core/styles';

export const useStyles = makeStyles((theme: Theme) => ({
  root: {
      width: '100%',
  },
  rightToolbarItem: {
    marginLeft: "auto",
    marginRight: -12
  },
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
    minWidth: 120,
    height: 24,
  },  
  boxHeader: {
      width: '100%',
      display: 'flex',
      [theme.breakpoints.down('md')]: {
          flexDirection: 'column',
      },
  },
  boxHeaderTitle: {
      padding: '5px 10px',
      fontSize: 35,
  },
  fillRemainingSpace: {
      flex: '1 1 auto',
      [theme.breakpoints.down('md')]: {
          display: 'none',
      },
  },
  avatar: {
      marginRight: 5
  },
  summary: {
      display: 'flex',
      alignContent: 'center',
      alignItems: 'center',
  },
  heading: {
      fontSize: theme.typography.pxToRem(20),
      height: '100%',
      verticalAlign: 'middle',
      flexBasis: '33.33%',
      flexShrink: 0,
      color: theme.palette.text.secondary,
  },
  secondaryHeading: {
      fontSize: theme.typography.pxToRem(20),
  },
  labelData: {
    fontSize: theme.typography.pxToRem(15),
  },
  column: {
    flexBasis: '33.33%',
  },
  helper: {
    borderLeft: `2px solid ${theme.palette.divider}`,
    padding: theme.spacing(1, 2),
  },
}));