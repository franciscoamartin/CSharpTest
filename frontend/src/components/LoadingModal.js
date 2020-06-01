import React from 'react';
import swal from 'sweetalert';
import ReactDOM from 'react-dom';
import ReactLoading from 'react-loading';

export default function showModal() {
  let wrapper = document.createElement('div');
  ReactDOM.render(
    <ReactLoading
      type="spinningBubbles"
      color="var(--color-red)"
      height="70px"
      width="70px"
    />,
    wrapper
  );
  let el = wrapper.firstChild;

  swal({
    title: 'Aguarde, por favor',
    content: el,
  });
}
