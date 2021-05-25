xcopy .\ClientApp\src\features\*.* "..\..\..\Elmah\ReactTS\ClientApp\src\features\PetStore\" /Y /S
xcopy .\ClientApp\src\apis\*.* "..\..\..\Elmah\ReactTS\ClientApp\src\apis\PetStore\" /Y /S
copy .\ClientApp\public\locales\en\UIStringResource.json "..\..\..\Elmah\ReactTS\ClientApp\public\locales\en\UIStringResource_PetStore.json" /Y
copy .\ClientApp\public\locales\es\UIStringResource.json "..\..\..\Elmah\ReactTS\ClientApp\public\locales\es\UIStringResource_PetStore.json" /Y

