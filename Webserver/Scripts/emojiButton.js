var input = document.querySelector('#emojiArea');
var picker = new EmojiButton({
    position: 'right-start'
});

picker.on('emoji', emoji => {
    input.value += emoji;
});

input.addEventListener('click', () => {
    picker.pickerVisible ? picker.hidePicker() : picker.showPicker(input);
});