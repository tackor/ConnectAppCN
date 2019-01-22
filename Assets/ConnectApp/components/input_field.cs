using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

namespace Unity.UIWidgets.Samples.ConnectApp.widgets {
    public class InputField : StatefulWidget {
        public InputField(
            Key key = null,
            TextEditingController controller = null,
            FocusNode focusNode = null,
            bool obscureText = false,
            bool autocorrect = true,
            TextStyle style = null,
            TextAlign textAlign = TextAlign.left,
            int maxLines = 1,
            bool autofocus = false,
            string hintText = null,
            TextStyle hintStyle = null,
            string labelText = null,
            TextStyle labelStyle = null,
            Color cursorColor = null,
            double cursorWidth = 2.0,
            ValueChanged<string> onChanged = null,
            ValueChanged<string> onSubmitted = null,
            EdgeInsets scrollPadding = null
        ) : base(key) {
            this.controller = controller;
            this.textAlign = textAlign;
            this.focusNode = focusNode;
            this.obscureText = obscureText;
            this.autocorrect = autocorrect;
            this.style = style;
            this.textAlign = textAlign;
            this.maxLines = maxLines;
            this.autofocus = autofocus;
            this.hintText = hintText;
            this.hintStyle = hintStyle;
            this.labelText = labelText;
            this.labelStyle = labelStyle;
            this.cursorWidth = cursorWidth;
            this.cursorColor = cursorColor;
            this.onChanged = onChanged;
            this.onSubmitted = onSubmitted;
            this.scrollPadding = scrollPadding;
        }

        public readonly TextEditingController controller;
        public readonly FocusNode focusNode;
        public readonly bool obscureText;
        public readonly bool autocorrect;
        public readonly TextStyle style;
        public readonly TextAlign textAlign;
        public readonly int maxLines;
        public readonly bool autofocus;
        public readonly string hintText;
        public readonly TextStyle hintStyle;
        public readonly string labelText;
        public readonly TextStyle labelStyle;
        public readonly Color cursorColor;
        public readonly double cursorWidth;
        public readonly ValueChanged<string> onChanged;
        public readonly ValueChanged<string> onSubmitted;

        public readonly EdgeInsets scrollPadding;
//        public readonly TextInputAction textInputAction;
//        public readonly TextInputType keyboardType;
//        public readonly Brightness keyboardAppearance;

        public override State createState() {
            return new _InputField();
        }
    }

    internal class _InputField : State<InputField> {
        TextEditingController _textEditingController;
        FocusNode _focusNode;
        bool _isHintTextHidden = false;

        public override void initState() {
            base.initState();
            this._textEditingController = new TextEditingController("");
            this._focusNode = new FocusNode();
        }

        public override Widget build(BuildContext context) {
            return new Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: new List<Widget> {
                    this._buildLabelText(),
                    new Stack(
                        children: new List<Widget> {
                            this._buildEditableText(), this._buildHintText()
                        }
                    )
                }
            );
        }

        Widget _buildLabelText() {
            if (this.widget.hintText == null || !this._isHintTextHidden) return new Container(height: 20);

            return new Container(
                height: 20,
                alignment: Alignment.bottomLeft,
                child: new Text(this.widget.hintText,
                    style: this.widget.labelStyle
                )
            );
        }

        Widget _buildHintText() {
            if (this.widget.hintText == null || this._isHintTextHidden) return new Container();
            return new Positioned(
                top: 0,
                left: 0,
                bottom: 0,
                child: new GestureDetector(
                    onTap: () => {
                        FocusNode focusNode = this.widget.focusNode ?? this._focusNode;
                        FocusScope.of(this.context).requestFocus(focusNode);
                    },
                    child: new Text(this.widget.hintText,
                        style: this.widget.hintStyle
                    )
                )
            );
        }

        Widget _buildEditableText() {
            return new GestureDetector(
                onTap: () => {
                    var focusNode = this.widget.focusNode ?? this._focusNode;
                    FocusScope.of(this.context).requestFocus(focusNode);
                },
                child: new EditableText(
                    maxLines: this.widget.maxLines,
                    controller: this.widget.controller ?? this._textEditingController,
                    focusNode: this.widget.focusNode ?? this._focusNode,
                    autofocus: this.widget.autofocus,
                    obscureText: this.widget.obscureText,
                    style: this.widget.style,
                    cursorColor: this.widget.cursorColor,
                    onChanged: str => {
                        var isTextEmpty = (str.Length > 0);
                        if (this._isHintTextHidden != isTextEmpty)
                            this.setState(() => { this._isHintTextHidden = isTextEmpty; });
                    }
                )
            );
        }
    }
}