import React from 'react';
import Container from '@material-ui/core/Container';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import Link from '@material-ui/core/Link';
import ProTip from './ProTip';

function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {'Copyright © '}
      <Link color="inherit" href="https://material-ui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

export default function App() {
  return (
    <Container maxWidth="sm">
      <Box my={4}>
      <Typography variant="h4" component="h1" gutterBottom>
          React 17.0.2
        </Typography>
        <Typography variant="h4" component="h1" gutterBottom>
          Material-UI ^4.11.4
        </Typography>
        <ProTip />
        <Copyright />
      </Box>
    </Container>
  );
}