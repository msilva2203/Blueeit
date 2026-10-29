import Block from "../components/Block";
import BlockContent from "../components/BlockContent";
import BodyHeader from "../components/BodyHeader";
import PageLayout from "../components/PageLayout";
import HelpSideNavPanel from "../components/panels/HelpSideNavPanel";
import OnlineMembersPanel from "../components/panels/OnlineMembersPanel";

export default function ContactPage() {
    const sideNavSelectionIndex = 2;

    return (
        <>
            <BodyHeader title={"Contact"}/>
            <PageLayout sidebar={<><OnlineMembersPanel/></>} sidenav={<><HelpSideNavPanel selectionIndex={sideNavSelectionIndex}/></>}>
                <Block>
                    <BlockContent />
                </Block>
            </PageLayout>
        </>
    );
}