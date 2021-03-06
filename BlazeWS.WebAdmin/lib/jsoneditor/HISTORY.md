# JSON Editor Online - History

http://jsoneditoronline.org


## 2013-05-27, version 2.2.1

- Fixed undefined options in TextEditor. Thanks Wiseon3.
- Fixed non-working save function on Firefox 21. Thanks youxiachai.


## 2013-05-04, version 2.2.0

- Unified JSONFormatter and JSONEditor in one editor with a switchable mode.
- Urls are navigable now.
- Improved error and log handling.
- Added jsoneditor to package managers npm and bower.


## 2013-03-11, version 2.1.1

- Fixed an issue with console outputs on IE8, causing the editor not to work
  at all on IE8.


## 2013-03-08, version 2.1.0

- Replaced the plain text editor with code editor Ace, which brings in syntax
  highlighting and code inspection.
- Improved the splitter between the two panels. Panels can be hided.


## 2013-02-26, version 2.0.2

- Fixed: dragarea of the root node was wrongly visible is removed now.


## 2013-02-21, version 2.0.1

- Fixed undefined variable in the redo method.
- Removed the "hide ads" button. Not allowed by Google AdSense, sorry.


## 2013-02-09, version 2.0.0

- Implemented a context menu, replacing the action buttons on the right side of
  the editor and the inline action buttons. This gives a cleaner interface,
  more space for the actual contents, and more room for new controls (like
  insert and sort).
- Implemented shortcut keys. The JSON Editor can be used with just a keyboard.
- Implemented sort action, which sorts the childs of an array or object.
- Implemented auto scrolling up and down when dragging a node and reaching
  the top or bottom of the editor.
- Added support for CommonJS and RequireJS.
- Added more examples.
- Improved performance and memory usage.
- Implemented a new mode 'form', in which only values are editable and the
  fields are fixed.
- Minor improvements and bug fixes.


## 2012-12-08, version 1.7.0

- Implemented two modes: 'editor' (default), and 'viewer'. In viewer mode,
  the data and datastructure is read-only.
- Implemented methods set(json, name), setName(name), and getName(), which
  allows for setting and getting the field name of the root node.
- Fixed an issue where the search bar does not work when there is no global
  window.editor object.


## 2012-11-26, version 1.6.2

- Fixed a bug in the change callback handler, resulting in an infinite loop
  when requesting the contents of the editor inside the callback (issue #19).


## 2012-11-21, version 1.6.1

- Added a request header "Accept: application/json" when loading files and urls.


## 2012-11-03, version 1.6.0

- Added feature to the web application to load and save files from disk and url.
- Improved error messages in the web application using JSONLint.
- Made the web application pass the W3C markup validation service.
- Added option 'change' to both editor and formatter, which allows to set a
  callback which is triggered when the contents of the editor or formatter
  changes.
- Changed the default indentation of the JSONFormatter to 4 spaces.
- Renamed options 'enableSearch' and 'enableHistory' to 'search' and 'history'
  respectively.
- Added parameter 'json' to the JSONFormatter constructor.
- Added option 'indentation' to the JSONFormatter.


## 2012-10-08, version 1.5.1

- Replaced the paid Chrome App with a free, hosted Chrome App (with ads).


## 2012-10-02, version 1.5.0

- Implemented history: undo/redo all actions.
- Created menu icons (instead of text buttons).
- Cleaned up the code (removed unused params, improved comments, etc).
- Minor performance improvements.


## 2012-08-31, version 1.4.4

- Changed: description of advertisement now gives information about the Chrome
  App (without ads).
- Changed: Chrome App is now configured to be available offline.
- Fixed: When zooming your browser window, the fields/values did get wrapped
  on Chrome (thanks Henri Gourvest), and on Firefox sometimes the jsoneditor
  disappeared due to wrapping of the interface contents.


## 2012-08-25, version 1.4.3

- Changed: changed code for the buttons to copy from formatter to editor and
  vice versa, no inline javascript (gives security policy errors in chrome app).


## 2012-08-25, version 1.4.2

- Changed: other bootstrapping mechanism for the chrome app, in a separate
  javascript file, as inline javascript is not allowed (security policy).
- Fixed: drop down menu for changing the field type did throw javascript errors
  (did not break any functionality though).


## 2012-08-23, version 1.4.1

- New: Chrome app created.


## 2012-08-23, version 1.4.0

- New: Improved icon, logo, and interface header.


## 2012-08-19, version 1.3.0

- New: Added buttons next and previous to the search box in the upper right.
- New: Escape characters are automatically inserted before " and \ missing
  and escape character, making the string contents valid JSON. New lines are
  automatically replaced with \n. (Thanks Steve Clay)
- Changed: all icons have been put in a single sprite. This will improve page
  load times as there are much less server requests needed to load the editor.


## 2012-08-12, version 1.2.0

- New: Added search functionality. Search results are expanded and highlighed.
  Quickkeys in the search box: Enter (next), Shift+Enter (previous), Ctrl+Enter
  (search again).
- New: The position of the vertical separator between left and right panel is
  stored.
- New: Link to the sourcecode on github added at the bottom of the page.
- Changed: Refinements in the layout: fonts, colors, icons.
- Fixed: leading an trailing spaces not being displayed in the editor.
- Fixed: wrapping of long words and urls in Chrome.
- Fixed: ignoring functions and undefined values in the loaded JSON object
  (they where interpreted as empty object and string instead of being ignored).


## 2012-07-01, version 1.1.1

- Fixed global event listener for the focus/blur events, causing changes in
  fields and values not always being registered.
- Fixed a css issue with Firefox (box-sizing of the editor).


## 2012-04-24, version 1.1

- Fixed a bug. Dragging an object down which has been expanded and collapsed
  again did not work.
- Using a minified version of jsoneditor.js, to improve page load time and
  save bandwidth.


## 2012-04-21, version 1.0

- Values are no longer aligned in one global column, but placed directly right
  from the field. Having field and value close together improves readability,
  especially in case of deeply nested data.
- Values are colorized by their type: strings are green, values read, booleans
  blue, and null is purple.
- Font is changed to a monotype font for better readability.
- Special characters like \t are now handled nicely.
- Overall performance and memory usage improved.
- When clicking on whitespace, focus is set to the closest field or value.
- some other small interface tweaks.
- Fixed a bug with casting a value from type auto to string and vice versa
  (the value was not casted at all).


## 2012-03-01, version 0.9.10

- Nicer looking select box for the field types, with icons.
- Improved drag and drop: better visualized, and now working in all browers.
- Previous values will be restored after changing the type of a field. When
  changing the type back, the previous value or childs will be restored.
- When hovering buttons (fieldtype, duplicate, delete, add) or when dragging
  a field, corresponding field including its childs is highlighted. This makes
  it easier to see what part of the data will be edited.
- Errors are now displayed in a message window on top of the page instead of
  an alert which pops up.
- Fixed a bug with displaying enters in fields.
- Fixed a bug where the last trailing enter was removed when setting json
  in the editor.
- Added a fix to get around Internet Explorer 8 issues with vertical scrollbars.


## 2012-01-29, version 0.9.9

- Fields can be duplicated
- Support for drag and drop:
  - fields in the editor itself can be moved via drag and drop
  - fields can be exported from the editor as JSON
  - external JSON can be dropped inside the editor
- When changing type from array to object and vice versa, childs will be
  maintained instead of removed.
- Updated interface. Works now in IE8 too.


## 2012-01-16, version 0.9.8

- Improved the performance of expanding a node with all its childs.


## 2012-01-09, version 0.9.7

- Added functionallity to expand/collapse a node and all its childs. Click
  the expand button of a node while holding Ctrl down.
- Small interface improvements


## 2011-11-28, version 0.9.6

- First fully usable version of the JSON editor