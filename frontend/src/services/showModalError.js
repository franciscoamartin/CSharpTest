import swal from 'sweetalert';

export default function showModalError(error, genericMessage) {
  swal(
    error.response ? error.response.data.message : genericMessage,
    '',
    'error'
  );
}
