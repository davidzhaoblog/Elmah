import { makeStyles, Theme } from '@material-ui/core/styles';

export const useStyles = makeStyles((theme: Theme) => ({
  root: {
      width: '100%',
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
      fontSize: theme.typography.pxToRem(15),
      height: '100%',
      verticalAlign: 'middle',
      flexBasis: '33.33%',
      flexShrink: 0,
      color: theme.palette.text.secondary,
  },
  secondaryHeading: {
      fontSize: theme.typography.pxToRem(15),
  },
}));