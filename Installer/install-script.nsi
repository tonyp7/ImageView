; --------------------------------
; ImageView Install Script
; --------------------------------

;--------------------------------
;Include Modern UI
  !include "MUI2.nsh"

;--------------------------------
;General

  ;Name and file
  Name "ImageView"
  OutFile "ImageViewSetup.exe"
  Unicode True

  ;Default installation folder
  ;InstallDir "$LOCALAPPDATA\ImageView"
  InstallDir "$PROGRAMFILES64\ImageView"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\ImageView" ""

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin


;--------------------------------
;Variables

  Var StartMenuFolder


;--------------------------------
;Interface Settings

  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Header\win.bmp"
  !define MUI_ABORTWARNING
  
;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "..\LICENSE"
  
  ;choose what to install
  !insertmacro MUI_PAGE_COMPONENTS

  ;choose directory to install
  !insertmacro MUI_PAGE_DIRECTORY
  
  ;install progress
  !insertmacro MUI_PAGE_INSTFILES
  
  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\ImageView" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
  
  ;option to run the program when setup finishes
  !define MUI_FINISHPAGE_RUN "$INSTDIR\ImageView.exe"
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "ImageView Program" SecMain

  SectionIn RO
  
  SetOutPath "$INSTDIR"
  
  ;install files
  File "..\ImageView\bin\Release\ImageView.exe"
  File "..\ImageView\bin\Release\Magick.Native-Q8-x64.dll"
  File "..\ImageView\bin\Release\Magick.NET-Q8-x64.dll"
  File "..\ImageView\bin\Release\SevenZipExtractor.dll"
  File "..\ImageView\bin\Release\LICENSE"
  File "..\ImageView\bin\Release\DEPENDENCIES.md"
  
  SetOutPath "$INSTDIR\x64"
  File "..\ImageView\bin\Release\x64\7z.dll"
  
  ; Set registry view to 64bit
  SetRegView 64
  
  ;Store installation folder
  WriteRegStr HKCU "Software\ImageView" "" $INSTDIR
  WriteRegStr HKLM "Software\ImageView" "" $INSTDIR
  
  ; Capabilities
  WriteRegStr HKLM "Software\ImageView\Capabilities" "ApplicationDescription" "Powerful yet easy to use image and photo viewer"
  WriteRegStr HKLM "Software\ImageView\Capabilities" "ApplicationName" "ImageView"
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" "" ""
  
  ;bmp
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".bmp" "ImageView.bmp"
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".dib" "IrfanView.bmp"
  ;png
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".png" "ImageView.png"
  ;jpg
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".jpg" "ImageView.jpg"
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".jpeg" "ImageView.jpg"
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".jpe" "ImageView.jpg"
  ;tif
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".tif" "ImageView.tif"
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".tiff" "ImageView.tif"
  ;gif
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".gif" "ImageView.gif"
  ;wdp
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".wdp" "ImageView.wdp"
  ;jfif
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".jfif" "ImageView.jfif"
  ;webp
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".webp" "ImageView.webp"
  ;cr2 (Canon Raw)
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".cr2" "ImageView.cr2"
  ;dnp (Digital Negative Raw)
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".dng" "ImageView.dng"
  ;jp2 (JPEG 2000)
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".jp2" "ImageView.jp2"
  ;psd
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".psd" "ImageView.psd"
  ;svg
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".svg" "ImageView.svg"
  ;tga
  WriteRegStr HKLM "Software\ImageView\Capabilities\FileAssociations" ".tga" "ImageView.tga"
  

  ;command
  WriteRegStr HKLM "Software\ImageView\shell" "" ""
  WriteRegStr HKLM "Software\ImageView\shell\open" "" ""
  WriteRegStr HKLM "Software\ImageView\shell\open\command" "" '"$INSTDIR\ImageView.exe"'
  
  ;register app
  WriteRegStr HKLM "Software\RegisteredApplications" "ImageView" "Software\ImageView\Capabilities"
  
  
  ; Class Root related registry entries
  WriteRegStr HKCR "ImageView" "" ""
  WriteRegStr HKCR "ImageView\shell" "" ""
  WriteRegStr HKCR "ImageView\shell\open" "" ""
  WriteRegStr HKCR "ImageView\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;bmp
  WriteRegStr HKCR "ImageView.bmp" "" ""
  WriteRegStr HKCR "ImageView.bmp\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.bmp\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;jpg
  WriteRegStr HKCR "ImageView.jpg" "" ""
  WriteRegStr HKCR "ImageView.jpg\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.jpg\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;png
  WriteRegStr HKCR "ImageView.png" "" ""
  WriteRegStr HKCR "ImageView.png\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.png\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;tif
  WriteRegStr HKCR "ImageView.tif" "" ""
  WriteRegStr HKCR "ImageView.tif\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.tif\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;gif
  WriteRegStr HKCR "ImageView.gif" "" ""
  WriteRegStr HKCR "ImageView.gif\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.gif\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;wdp
  WriteRegStr HKCR "ImageView.wdp" "" ""
  WriteRegStr HKCR "ImageView.wdp\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.wdp\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;jfif
  WriteRegStr HKCR "ImageView.jfif" "" ""
  WriteRegStr HKCR "ImageView.jfif\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.jfif\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;webp
  WriteRegStr HKCR "ImageView.webp" "" ""
  WriteRegStr HKCR "ImageView.webp\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.webp\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;cr2
  WriteRegStr HKCR "ImageView.cr2" "" ""
  WriteRegStr HKCR "ImageView.cr2\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.cr2\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;dnp
  WriteRegStr HKCR "ImageView.dnp" "" ""
  WriteRegStr HKCR "ImageView.dnp\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.dnp\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;jp2
  WriteRegStr HKCR "ImageView.jp2" "" ""
  WriteRegStr HKCR "ImageView.jp2\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.jp2\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;psd
  WriteRegStr HKCR "ImageView.psd" "" ""
  WriteRegStr HKCR "ImageView.psd\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.psd\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;svg
  WriteRegStr HKCR "ImageView.svg" "" ""
  WriteRegStr HKCR "ImageView.svg\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.svg\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  ;tga
  WriteRegStr HKCR "ImageView.tga" "" ""
  WriteRegStr HKCR "ImageView.tga\DefaultIcon" "" "$INSTDIR\ImageView.exe,0"
  WriteRegStr HKCR "ImageView.tga\shell\open\command" "" '"$INSTDIR\ImageView.exe" "%1"'
  
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  
  ;Start menu shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
  CreateShortcut "$SMPROGRAMS\$StartMenuFolder\ImageView.lnk" "$INSTDIR\ImageView.exe"
  CreateShortcut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecMain ${LANG_ENGLISH} "This is the core ImageView program."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SecMain} $(DESC_SecMain)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ;install files
  Delete "$INSTDIR\Uninstall.exe"
  Delete "$INSTDIR\ImageView.exe"
  Delete "$INSTDIR\LICENSE"
  Delete "$INSTDIR\DEPENDENCIES.md"
  Delete "$INSTDIR\Magick.Native-Q8-x64.dll"
  Delete "$INSTDIR\Magick.NET-Q8-x64.dll"
  Delete "$INSTDIR\SevenZipExtractor.dll"
  Delete "$INSTDIR\x64\7z.dll"

  
  ;attempt to delete the config file that is auto-generated
  Delete "$LOCALAPPDATA\ImageView\config.xml"
  
  
  ;delete install folder
  RMDir "$INSTDIR\x64"
  RMDir "$INSTDIR"
  
  ;remove start menu shortcuts
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
  Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\ImageView.lnk"
  RMDir "$SMPROGRAMS\$StartMenuFolder"

  ;DeleteRegKey /ifempty HKCU "Software\ImageView"
  SetRegView 64
  DeleteRegKey HKCU "Software\ImageView"
  DeleteRegKey HKLM "Software\ImageView"
  DeleteRegKey HKCR "ImageView"
  DeleteRegKey HKCR "ImageView.bmp"
  DeleteRegKey HKCR "ImageView.jpg"
  DeleteRegKey HKCR "ImageView.png"
  DeleteRegKey HKCR "ImageView.tif"
  DeleteRegKey HKCR "ImageView.gif"
  DeleteRegKey HKCR "ImageView.wdp"
  DeleteRegKey HKCR "ImageView.jfif"
  DeleteRegKey HKCR "ImageView.webp"
  DeleteRegKey HKCR "ImageView.cr2"
  DeleteRegKey HKCR "ImageView.dnp"
  DeleteRegKey HKCR "ImageView.jp2"
  DeleteRegKey HKCR "ImageView.psd"
  DeleteRegKey HKCR "ImageView.svg"
  DeleteRegKey HKCR "ImageView.tga"
  

SectionEnd