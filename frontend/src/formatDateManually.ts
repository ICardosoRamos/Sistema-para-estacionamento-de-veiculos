const formatDateManually = (
  e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
) => {
  const inputValue = e.target.value;

  // Ensure only numbers and "/" and ":" are allowed
  if (/^[0-9/:\s]*$/.test(inputValue)) {
    // Construct the formatted date string
    let formattedValue = "";

    // Iterate over the input value and format it according to dd/MM/yyyy HH:mm
    for (let i = 0; i < inputValue.length && formattedValue.length < 16; i++) {
      const char = inputValue.charAt(i);
      switch (formattedValue.length) {
        case 0:
        case 1:
          if (/[0-3]/.test(char)) {
            formattedValue += char;
          }
          break;
        case 2:
          if (formattedValue.charAt(1) === "3") {
            if (/[0-1]/.test(char)) {
              formattedValue += char + "/";
            }
          } else {
            formattedValue += char + "/";
          }
          break;
        case 3:
          if (/[0-1]/.test(char)) {
            formattedValue += char;
          }
          break;
        case 5:
          if (/[0-1]/.test(char)) {
            formattedValue += char + "/";
          }
          break;
        case 6:
          if (/[0-2]/.test(char)) {
            formattedValue += char;
          }
          break;
        case 8:
        case 9:
          if (/[0-9]/.test(char)) {
            formattedValue += char;
          }
          break;
        case 11:
          if (/[0-2]/.test(char)) {
            formattedValue += char + ":";
          }
          break;
        case 12:
        case 14:
          if (/[0-5]/.test(char)) {
            formattedValue += char;
          }
          break;
        case 13:
        case 15:
          if (/[0-9]/.test(char)) {
            formattedValue += char;
          }
          break;
        default:
          break;
      }
    }

    return formattedValue;
  }
};

export default formatDateManually;
