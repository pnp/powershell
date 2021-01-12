# Consent results

<script type="text/javascript">
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    var success = urlParams.get('admin_consent')
    if(success.toLowerCase() === 'true')
    {
        document.write("You consented successfully for your newly created Azure AD Application. You may close this browser now.");
    } else {
        document.write("You did not consent successfully for your newly created Azure AD Application. Please navigate to your Azure AD, find the application and provide consent there.");
    }
</script>