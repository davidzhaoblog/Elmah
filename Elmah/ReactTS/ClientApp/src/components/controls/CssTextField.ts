import { withStyles } from "@material-ui/core/styles";
import TextField from "@material-ui/core/TextField";

const mingColor = '#387780';
const dartmouthGreenColor = '#2D7638';
const emeraldGreenColor = '#62C370';

export const CssTextField = withStyles({
  root: {
    '& label.Mui-focused': {
      color: mingColor,
    },
    '& .MuiInput-underline:after': {
      borderBottomColor: dartmouthGreenColor,
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: dartmouthGreenColor,
      },
      '&:hover fieldset': {
        borderColor: emeraldGreenColor,
      },
      '&.Mui-focused fieldset': {
        borderColor: mingColor,
      },
    },
  },
})(TextField);