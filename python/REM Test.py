from PCI_REM import renderImage, renderData, renderer, editor

renderImage("test.pci")

renderData([
    "0123456789ABCDEF",
    "FEDCBA9876543210"
])

renderer()

editor()